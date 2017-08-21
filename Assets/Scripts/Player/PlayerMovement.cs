using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 4.0f;

    Rigidbody playerBody;
    int floorMask;
    float camRayLength = 100f;

    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // move player based on current speed
        Vector3 horizontalDir = Camera.main.transform.right;
        Vector3 verticalDir = Vector3.Cross(Vector3.down, Camera.main.transform.right);
        Vector3 movement = (horizontalDir * Input.GetAxis("Horizontal"))
            + (verticalDir * Input.GetAxis("Vertical"));
        transform.Translate(movement * MovementSpeed, Space.World);

        turning();
    }

    void turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerBody.MoveRotation(newRotation);
        }
    }
}
