using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float walkSpeed = 9f; // This is for walking and Sprinting
    public float sprintSpeed = 14f;
    public float defaultSpeed = 9f;
    public float defaultWalkSpeed = 4f;

    private bool isCrouching;
    private Vector3 originalCenter;
    private float originalHeight;
    private float originalMoveSpeed;

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
        transform.tag = "Player";
        controller = GetComponent<CharacterController>();
        originalCenter = controller.center;
        originalHeight = controller.height;
        originalMoveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded =
            Physics.CheckSphere(groundCheck.position, groundDistance,
                groundMask); //This is the Groundcheck for Jumping. This is required for jumping

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //This is for Jumping

        controller.Move(move * walkSpeed * Time.deltaTime);
        controller.Move(move * defaultSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            defaultSpeed = sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            defaultSpeed = defaultWalkSpeed;
        }
 
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = 1f;
            controller.center = new Vector3(0.5f, 0.25f, 0.5f);
            walkSpeed = 1f;
            isCrouching = true;
        }

        if (!Input.GetKey(KeyCode.LeftControl) && isCrouching)
        {
            controller.height = originalHeight;
            controller.center = originalCenter;
            walkSpeed = originalMoveSpeed;
            isCrouching = false;
        }

        if (!Input.GetKey(KeyCode.LeftControl) && isCrouching)
        {
            Vector3 point0 = transform.position + originalCenter - new Vector3(0.0f, originalHeight, 0.0f);
            Vector3 point1 = transform.position + originalCenter + new Vector3(0.0f, originalHeight, 0.0f);
            if (Physics.OverlapCapsule(point0, point1, controller.radius).Length == 0)
            {
                controller.height = originalHeight;
                controller.center = originalCenter;
                walkSpeed = originalMoveSpeed;
                isCrouching = false;
            }
        }
    }
}
