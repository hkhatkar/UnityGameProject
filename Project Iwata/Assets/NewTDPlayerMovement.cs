using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTDPlayerMovement : Character
{//This script is the Main player movement script for the open 3D aspect of the game.
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    public float moveSpeed;
    private Rigidbody myRigidbody;
    public CharacterController controller;
    public float gravityScale;
    float h;
    float v; 
    public float jumpPower;
    float currentJumpPower;

    [HideInInspector]
    public bool canMove = true;
    public static Vector3 direction = Vector3.forward;

    protected override void Start()
    {
        myRigidbody = GetComponentInParent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        base.Start();
    }

    protected override void Update()
    {
        GetInput();
        base.Update();
    }

    private void GetInput()
    {
        direction = Vector3.zero;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        direction -= transform.right * h * speed * Time.deltaTime;
        direction -= transform.forward * v * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {//basic jump
            currentJumpPower = jumpPower;    
        }

        //Used for debugging and traversing the map quickly when testing
       if (Input.GetKeyDown(KeyCode.M))
       { 
            currentJumpPower = 0.01f;
            Character.speed = 340f;
            gravityScale = 0.01f;
        }
        //-------------------------------------------------------------

         if (Input.GetKey(KeyCode.LeftShift) && gravityScale != 0.01f)
        {//increase speed (run)
            Character.speed = 45f;
        }
        else if (gravityScale != 0.01f)
        {
            Character.speed = 30f;
        }

        currentJumpPower -= gravityScale * Time.deltaTime;
        direction.y = currentJumpPower;

        controller.Move(direction * moveSpeed * Time.deltaTime);
        //basic movement controller 

        float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
    }
 
}
