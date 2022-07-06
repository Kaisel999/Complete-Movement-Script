using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : MonoBehaviour
{

    private NewPlayerMovement movement;
    private CharacterController controller;
    private Crouching crouch;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<NewPlayerMovement>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movement.walkSpeed = movement.sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movement.walkSpeed = 9f;
        }
    }
}
