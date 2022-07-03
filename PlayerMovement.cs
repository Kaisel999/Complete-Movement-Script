using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float walkSpeed = 12f; // This is for walking and Sprinting
    public float sprintSpeed = 20f;

    public float gravity = -9.81f; // This is for Jumping and Gravity
    public float jumpHeight = 2f;

    public Transform groundCheck; // This is the groundcheck for jumping
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;

    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //This is the Groundcheck for Jumping. This is required for jumping

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //This is for Jumping

        controller.Move(move * walkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift)) //This is for Sprinting
        {
            walkSpeed = sprintSpeed;
        }
        else
        {
            walkSpeed = walkSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkSpeed = 12f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) //This is for Crouching and Crouch Sprinting
        {
            transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
            walkSpeed = 6f;
            sprintSpeed = 9f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            walkSpeed = 12f;
            sprintSpeed = 20f;
        }
    }
}
