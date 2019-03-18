using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerMovement : MonoBehaviour {

    public Stat Statis;
    public bool canMove;

    public RaycastHit2D hitInfo;
    public float JUMPcooldownTime = 1;
    private float JUMPnextFireTime = 0;

    public GameObject ladderBox;
    public GameObject ladderPlatform;
    public GameObject Currency;
    public GameObject blood;
    public GameObject SmallBullet;
    public bool Swing = false;
   

    public float distance;
    public LayerMask whatIsLadder;
   public bool isClimbing;
    private float inputVertical;
    public static Rigidbody2D rb;
    public float speed;
    public bool preventJump = false;
    

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;
    public static float runSpeed = 40f;
    public bool jump = false;
    public bool crouch = false;
    public Dodge dodge;

    public bool grounded;

    [SerializeField]
    private Stat health;


    [SerializeField]
    private float initialHealth;

    public CameraShake cameraShake;

    public GameObject SwingRope;
    public GameObject SwingPrefab;
    public Pendulum ThisRope;

    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockFromRight;

    public Collider2D MainPlayerBodyCollider;



    public void Start()
    {

        
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        health.Initialize(initialHealth, initialHealth);

        
        ThisRope = SwingRope.GetComponent<Pendulum>();
       // SwingPrefab = GameObject.FindGameObjectWithTag("Swing").GetComponent<Transform>




    }



    // Update is called once per frame
    void Update () 
    {
      
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;

        }
        
        if (knockbackCount <= 0)//passed from other script
        {
           
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //THIS LINE ONLY IS RESPONSIBLE FOR MOVEMENT !!!!!!!!!!!!!!!!!!!!!!!
        }
        else
        {
            
            if(knockFromRight)
            {
                rb.velocity = new Vector2(knockback, knockback);//knock player back at opposite direction
            }
            if(!knockFromRight)
            {
                rb.velocity = new Vector2(-knockback, knockback);//knock player back at opposite direction
            }
            knockbackCount -= Time.deltaTime;
        }
        
    
    

        

          if (!PauseMenu.GameIsPaused)
        {
            animator.SetFloat("PLATSpeed", Mathf.Abs(horizontalMove));
        }
        //if (Input.GetButton("Jump"))
        //{
        //    if (Time.time > JUMPnextFireTime)
        //  {
        if (Input.GetButton("Jump"))
            {
                
             
                animator.SetBool("PLATJump", true);
               // JUMPnextFireTime = Time.time + JUMPcooldownTime;
            }

            if (preventJump == false)
            {

                if (Input.GetButton("Jump") && (!Input.GetKeyDown(KeyCode.W) && hitInfo.collider == null))
                {

                 if (!(ladderBox.CompareTag("Bars")))//checks if they are bars
                 {
                    isClimbing = false;
                 }
                    crouch = false;
                    jump = true;
                    


                }
            }
       // }
        else
        {
            jump = false;
        }

        if (jump == false)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                
                if (crouch == false)
                {
                    crouch = true;
                  
                }
                else if (crouch == true)
                {
                    crouch = false;
                   
                }
                health.MyCurrentValue += 5;//REMOVE THIS LATER THIS IS JUST FOR DEBUGGING TO SEE HOW HEAL WORKS

            }
          //  else if (Input.GetButtonUp("Crouch"))
           // {
          //      crouch = false;
          //  }
        }
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("PLATx", true);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("PLATx", false);
            }

        }
        HandlePLATLayers();

    }

    public void OnLanding()
    {
        if (!(ladderBox.CompareTag("Bars")))//checks if they are bars
        {
            ladderBox.SetActive(true);
        }
        animator.SetBool("PLATJump", false);
       // if (jump == true)
        //{
        //    JUMPnextFireTime = Time.time + JUMPcooldownTime;
      //  }
    }
    public void OnCrouching (bool PLATCrouch)
    {
        animator.SetBool("PLATCrouch", PLATCrouch);
    }

    public void HandlePLATLayers()
    {
        if (!PauseMenu.GameIsPaused)
        {

            if (isClimbing)
            {
                
                    ActivatePLATLayer("PLATClimbLayer");
            }
            else if (crouch)
            {
                ActivatePLATLayer("PLATCrouchLayer");
            }
            else if (Dodge.playDodgeAnim == true)
            {
                
                ActivatePLATLayer("PLATDodgeLayer");
            }
            else
            {
                ActivatePLATLayer("PLATIdleLayer");
            }
        }
        
       
    }

    public void ActivatePLATLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);

    }

    void FixedUpdate() //called a fixed amount of times per second
    {
        if(isClimbing == true)
        {
            crouch = false;
        }
      

        //move platformer character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        

            jump = false;


       // Physics2D.queriesStartInColliders = true;


        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        


            Debug.DrawRay(transform.position, Vector2.up, Color.red, distance);




         if (hitInfo.collider != null)
                {
            
            Debug.Log("IN!");
           
                    preventJump = true;
                    
                
                    if (Input.GetKey(KeyCode.W) && ! Input.GetButton("Jump"))
                    {
                        isClimbing = true;
                                        
                         jump = true;
                    }

                }
                else
                {
                    
                    isClimbing = false;
                    
                }

                if (isClimbing == true && hitInfo.collider != null)
                {
            ladderPlatform.SetActive(false);
                    preventJump = true;
                    inputVertical = Input.GetAxisRaw("Vertical");
                    rb.velocity = new Vector2(rb.velocity.x, (inputVertical * speed));


                    rb.gravityScale = 0;
                    animator.SetFloat("PLATSpeedUp", inputVertical);
                }
                else
            {
            ladderPlatform.SetActive(true);
            hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
            rb.gravityScale = 5;
            preventJump = false;
            }
              
           
         
          
              
             
         
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {

           
            knockbackCount = knockbackLength;
            if(col.transform.position.x < transform.position.x)
            {
                knockFromRight = true; //MIGHT HAVE TO CHANGE VAR NAME NOT SURE CHECK BEFORE CHANGING

            }
            else
            {
                knockFromRight = false;
            }
            if (ShieldRotation.Blocked == false)
            {
                health.MyCurrentValue -= 5;
                Instantiate(blood, transform.position, Quaternion.identity);
            }
          //  else if (ShieldRotation.Blocked == false ||  )
           // {

         //   }
            
            StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
            // force is how forcefully we will push the player away from the enemy.
            


        }
        if (col.gameObject.tag.Equals("Spikes"))
        {

            Instantiate(blood, transform.position, Quaternion.identity);

            health.MyCurrentValue -= 10;
            StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
            // force is how forcefully we will push the player away from the enemy.



        }


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Swing"))
        {
          
            ThisRope.Swinging = true;
        }
        else
        {
            ThisRope.Swinging = false;
        }
    }

}
