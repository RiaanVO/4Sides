using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1.75f;
    public float MovementSpeedWhileShooting = 1f;
    public Light MoveLight;
    public AudioListener Listener;

    private PlayerShooting weapons;
    private Rigidbody body;

    void Start()
    {
        weapons = GetComponent<PlayerShooting>();
        body = GetComponent<Rigidbody>();

        if (Listener != null)
        {
            Instantiate(Listener);
        }
    }

    void FixedUpdate()
    {
        // calculate world position of mouse cursor
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, Vector3.zero);
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

        // slow player's movement if they are shooting
        float movementSpeed = MovementSpeed;
        if (weapons != null && weapons.IsShooting)
        {
            movementSpeed = MovementSpeedWhileShooting;
        }
        if (MoveLight != null)
        {
            MoveLight.intensity = movement.magnitude * (Mathf.Sin(Time.time * 20) + 1);
        }

        body.AddForce(movement * movementSpeed, ForceMode.Impulse);
    }
}
