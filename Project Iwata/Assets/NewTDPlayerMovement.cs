using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTDPlayerMovement : Character
{
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
   // Vector3 movement;

    [HideInInspector]
    public bool canMove = true;
    public static Vector3 direction = Vector3.forward;
    // Start is called before the first frame update
    protected override void Start()
    {
        myRigidbody = GetComponentInParent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        base.Start();



    }

    // Update is called once per frame
    protected override void Update()
    {
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput * moveSpeed;
        GetInput();




        
        base.Update();
    }

    private void GetInput() //FOR 2D only
    {
        direction = Vector3.zero;
        // direction = Vector3.zero;

        //direction = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0f, Input.GetAxisRaw("Vertical") * speed);

        //  movement = Vector3.zero;
        v = Input.GetAxis("Vertical");
         h = Input.GetAxis("Horizontal");

        // movement -= transform.forward * v * speed * Time.deltaTime;
        //movement -= transform.right * h * speed * Time.deltaTime;
        //  movement = new Vector3(v * speed, h * speed);
        //  movement += Physics.gravity * gravityScale;
        //direction.y = direction.y + (Physics.gravity.y * gravityScale);

        // direction = new Vector3(h, 0, v);

        // controller.Move(movement);
        direction -= transform.right * h * speed * Time.deltaTime;
        direction -= transform.forward * v * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentJumpPower = jumpPower;
           
        }
       if (Input.GetKeyDown(KeyCode.M))
       {
            
            currentJumpPower = 0.01f;
            Character.speed = 340f;
            gravityScale = 0.01f;
        }

         if (Input.GetKey(KeyCode.LeftShift) && gravityScale != 0.01f)
        {
            Character.speed = 45f;
        }
        else if (gravityScale != 0.01f)
        {
            Character.speed = 30f;
        }

        currentJumpPower -= gravityScale * Time.deltaTime;
        direction.y = currentJumpPower;

        controller.Move(direction * moveSpeed * Time.deltaTime);

        float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
        //moveDirection = (forward * curSpeedX) + (right * curSpeedY);

    }
    // void FixedUpdate()
    // {
    // myRigidbody.velocity = moveVelocity;
    // }
  
    
}
