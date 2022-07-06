using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{

    private CharacterController controller;
    private Sprinting sprint;
    private NewPlayerMovement movement;
    
    private bool isCrouching;
    private Vector3 originalCenter;
    private float originalHeight;
    private float originalMoveSpeed;
    
    private float walkSpeed = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Player";
        controller = GetComponent<CharacterController>();
        movement = GetComponent<NewPlayerMovement>();
        sprint = GetComponent<Sprinting>();
        originalCenter = controller.center;
        originalHeight = controller.height;
        originalMoveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = 1f;
            controller.center = new Vector3(0.5f, 0.25f, 0.5f);
            walkSpeed = 3f;
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            sprint.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            sprint.enabled = true;
        }
    }
    }

