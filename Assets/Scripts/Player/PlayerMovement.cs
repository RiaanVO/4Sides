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

    bool usingJoy = false;
    Vector2 previousMousePos;

    void Start()
    {
        weapons = GetComponent<PlayerShooting>();
        body = GetComponent<Rigidbody>();

        if (Listener != null)
        {
            Instantiate(Listener);
        }

        previousMousePos = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        // calculate world position of mouse cursor
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0.0f;

        if (plane.Raycast(ray, out distance))
        {
            float rotation = 0;

            Vector3 mousePos = ray.GetPoint(distance);
            float rJoyX = Input.GetAxis("Right Joy X");
            float rJoyY = Input.GetAxis("Right Joy Y");

            // calculate rotation based on direction to mouse cursor
            if (rJoyX != 0 || rJoyY != 0)
            {
                rotation = -Mathf.Atan2(rJoyX, rJoyY) * Mathf.Rad2Deg + 45;
                body.MoveRotation(Quaternion.Euler(0.0f, rotation + 90, 0.0f));
                // Debug.Log(usingJoy + ": " + rJoyX + " | " + rJoyY);
                usingJoy = true;
                rJoyX = 0f;
                rJoyY = 0f;
            }
            else
            {
                Vector2 currentMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if (Vector2.Distance(currentMouse, previousMousePos) > 2)
                {
                    usingJoy = false;
                    // Debug.Log(usingJoy + ": " + Vector2.Distance(currentMouse, previousMousePos));

                }
                previousMousePos = currentMouse;
            }

            if (!usingJoy)
            {
                Vector3 dirToMouse = mousePos - transform.position;
                rotation = -Mathf.Atan2(dirToMouse.z, dirToMouse.x) * Mathf.Rad2Deg;
                body.MoveRotation(Quaternion.Euler(0.0f, rotation + 90, 0.0f));
            }

            // set the player rotation
            //body.MoveRotation(Quaternion.Euler(0.0f, rotation + 90, 0.0f));
            body.angularVelocity = Vector3.zero;
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
