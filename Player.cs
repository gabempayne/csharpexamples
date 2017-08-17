using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Visuals")]
    public GameObject model;
    public float rotationSpeed = 2f;

    [Header("Movement")]
    public float movementVelocity;
    public float speed = 5f;
    public float jumpVelocity = 150f;

    [Header("Equipment")]
    public Sword sword;


    private Rigidbody rb;
    private bool canJump;
    private Quaternion targetModelRotation;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Update()

    {   // Raycast to identify if player can jump.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f))
        {
            canJump = true;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime * rotationSpeed);

        ProcessInput();
    }

    void ProcessInput()
    {
        // Move in the XZ plane.
        rb.velocity = new Vector3(
            0,
            rb.velocity.y,
            0
            );
        if (Input.GetKey("right"))
        {
            rb.velocity = new Vector3(
                movementVelocity,
                rb.velocity.y,
                rb.velocity.z
                );
            // Rotating to right
            targetModelRotation = Quaternion.Euler(0, 270, 0);
        }

        if (Input.GetKey("left"))
        {
            rb.velocity = new Vector3(
                -movementVelocity,
                rb.velocity.y,
                rb.velocity.z
                );
            // Rotating to left
            targetModelRotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey("up"))
        {
                rb.velocity = new Vector3(
                    rb.velocity.x,
                    rb.velocity.y,
                    movementVelocity
                    );
            // Rotating up
            targetModelRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey("down"))
        {
                rb.velocity = new Vector3(
                    rb.velocity.x,
                    rb.velocity.y,
                    -movementVelocity
                    );
            // Rotating down
            targetModelRotation = Quaternion.Euler(0, 0, 0);
        }

        if (canJump && Input.GetKeyDown("space"))
        {
                canJump = false;
                rb.velocity = new Vector3(
                    rb.velocity.x,
                    jumpVelocity,
                    rb.velocity.z);
        }

        // Check equipment interaction.
        if (Input.GetKeyDown("z"))
        {
            sword.Attack();
        }
        
    }
}
