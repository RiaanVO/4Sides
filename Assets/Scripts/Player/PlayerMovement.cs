using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float normalMovementSpeed = 1.5f;
    public float slowMovementSpeed = 1f;
    public Light MoveLight;

    private PlayerShooting weapons;
    private Rigidbody body;

    void Start()
    {
        weapons = GetComponent<PlayerShooting>();
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // calculate world position of mouse cursor
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float distance = 0.0f;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 mousePos = ray.GetPoint(distance);

            // calculate rotation based on direction to mouse cursor
            Vector3 dirToMouse = mousePos - transform.position;
            float rotation = -Mathf.Atan2(dirToMouse.z, dirToMouse.x)
                * Mathf.Rad2Deg;

            // set the player rotation
            body.MoveRotation(Quaternion.Euler(0.0f, rotation + 90, 0.0f));
        }

        // move player based on current speed
        Vector3 horizontalDir = Camera.main.transform.right;
        Vector3 verticalDir = Vector3.Cross(Vector3.down, Camera.main.transform.right);
        Vector3 movement = (horizontalDir * Input.GetAxis("Horizontal"))
            + (verticalDir * Input.GetAxis("Vertical"));
        movement.Normalize();

        float movementSpeed = normalMovementSpeed;
        if (weapons != null && weapons.IsShooting)
        {
            movementSpeed = slowMovementSpeed;
        }
        if (MoveLight != null)
        {
            MoveLight.intensity = movement.magnitude * (Mathf.Sin(Time.time * 20) + 1);
        }

        body.AddForce(movement * movementSpeed, ForceMode.VelocityChange);
    }
}
