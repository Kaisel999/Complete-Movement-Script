using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    public float walkSpeed = 12f;
    public float sprintSpeed = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
