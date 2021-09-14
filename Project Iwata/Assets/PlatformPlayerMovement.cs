using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//imports Unity engine 

public class PlatformPlayerMovement : MonoBehaviour {
    //This class is reponsible for linking all the Player's attributes into one class where each one is implemented to the player


    private AirShot AirShotScript;//AIR SHOT

    public bool DmgTaken = false;
    //for boss ai on ln 390 - 405
    public Stat Statis;
    public bool canMove;

    public RaycastHit2D hitInfo;
    public float JUMPcooldownTime = 1;
    private float JUMPnextFireTime = 0;

    public GameObject ladderBox;
    public GameObject ladderPlatform;
    //Ladder objects that are assigned from other classes

    public GameObject Currency;
    public GameObject blood;
    public GameObject SmallBullet;
    public bool Swing = false;
   

    public float distance;
    public LayerMask whatIsLadder;
    public bool isClimbing;
    private float inputVertical;
    public static Rigidbody rb;
    public float speed;
    public bool preventJump = false;
    

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;
    public static float runSpeed = 70f;
    private bool jump = false;
    public bool crouch = false;
    public Dodge dodge;

    //public bool grounded;//Not needed?----------------------_____---____-____--___--__---_

    [SerializeField]
    private Stat health;


    [SerializeField]
    private float initialHealth;

    public CameraShake cameraShake;

    public static GameObject SwingRope;
    public GameObject SwingPrefab;
    public static Pendulum ThisRope;

    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockFromRight;

    public Collider2D MainPlayerBodyCollider;
    public static bool FacingLeft;//>> Lift Object

    //Most variables that would be used within different classes will be assigned here
    //Many of the variable are made PUBLIC. This is to assign certain game objects outside the class into a way that is understood within the class



    public void Start()
    {//This is the start procedure. Everything with in this procedure is the first components that need to be ran, before the rest of the class

        AirShotScript = GameObject.FindObjectOfType<AirShot>();
        rb = GetComponent<Rigidbody>();
        //assigns rb to a new RigidBody2D component which will be used to control forces applied on the player 
        canMove = true;
        health.Initialize(initialHealth, initialHealth);
        //Initializes the players health to full health at the start of procedure
        //This is linked to another separate class
        health.MyCurrentValue += 0;//REMOVE THIS LATER THIS IS JUST FOR DEBUGGING TO SEE HOW HEAL WORKS


    }



    // Update is called once per frame
    void Update () 
    {
       // Debug.Log("gravity scale" + rb.gravityScale);
     
       
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
            //if player cannot move the velocity is returned as 0
        }
        
        if (knockbackCount <= 0)//passed from other script
        {//when knockback count is equal to 0, the knockback force from enemies are not being applied
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            //This line is responsible for horizontal movement
        }
        else
        {     
            if(knockFromRight)
            {//If the player is hit by an enemy from the right direction
                rb.velocity = new Vector3(knockback*3, -8,0);//knock player back at opposite direction
            }
            if(!knockFromRight)
            {//If the player is hit by an enemy from the leftdirection
                rb.velocity = new Vector3(-knockback*3,-8,0);//knock player back at opposite direction
            }
            knockbackCount -= Time.deltaTime;
            //A velocity will be applied to the playerfor a short amount of time until knockbackCount is equal to 0
        }        

          if (!PauseMenu.GameIsPaused)
        {
            animator.SetFloat("PLATSpeed", Mathf.Abs(horizontalMove));
            //If the game is not paused the speed is set to an absolute value of horizontalMove
        }       
        if (Input.GetButton("Jump"))
            {//If the player presses the "Jump" button     
                animator.SetBool("PLATJump", true);
            //The Jump animation is played and set to active
            }

            if (preventJump == false)
            {//if nothing is preventing jump..

                if (Input.GetButton("Jump") && (!Input.GetKeyDown(KeyCode.W) && hitInfo.collider == null))
                {//if the jump button is pressed, the W button is not pressed and hit collider contains nothing

                 if (!(ladderBox.CompareTag("Bars")))
                 {//If the climable object are not tagged as "Bars"
                    isClimbing = false;
                    //Climbing is disabled
                 }
                    crouch = false;
                    jump = true;
                   
                }
            }
     
        else
        {
            jump = false;
        }

        if (jump == false)
        {
            if (Input.GetMouseButtonDown(0)) //"Crouch"
            {                
               // if (crouch == false)
              //  {
                    crouch = true;                  
              //  }
              //  else if (crouch == true)
              //  {
              //      crouch = false;                   
              //  }
              // health.MyCurrentValue += 0;//REMOVE THIS LATER THIS IS JUST FOR DEBUGGING TO SEE HOW HEAL WORKS
            }     
            else if (Input.GetMouseButtonUp(0))
            {
                crouch = false;
            }
        }
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {//if the pause menu isnt paused and A is entered
                animator.SetBool("PLATx", true);
                FacingLeft = true;
                Debug.Log("L");
                //Player moves left
            }
            if (Input.GetKeyDown(KeyCode.D))
            {//if the pause menu isnt paused and D is entered
                animator.SetBool("PLATx", false);
                FacingLeft = false;
                Debug.Log("R");
                //Player moves right
            }
        }
        HandlePLATLayers();
        //Handles the animation layers when walking

        AirShotScript.checkIfCrouching(crouch);//AIRSHOT
    }
    public void OnLanding()
    {//procedure for when the player lands back on the ground after jumping
        if (!(ladderBox.CompareTag("Bars")))
        {
            ladderBox.SetActive(true);
            //Ladders are climable again
        }
        animator.SetBool("PLATJump", false);
     
    }//end procedure
    public void OnCrouching (bool PLATCrouch)
    {
        animator.SetBool("PLATCrouch", PLATCrouch);
        //Crouch animation is activated
    }

    public void HandlePLATLayers()
    {
        if (!PauseMenu.GameIsPaused)
        {

            if (isClimbing)
            {//Climb animation played when climbing
                
                ActivatePLATLayer("PLATClimbLayer");
            }
            else if (crouch)
            {//crouch animation played when in aim mode
                ActivatePLATLayer("PLATCrouchLayer");
            }
            else if (Dodge.playDodgeAnim == true)
            {//dodge animation played when dodging
                
                ActivatePLATLayer("PLATDodgeLayer");
            }
            else
            {//idle animation played on default
                ActivatePLATLayer("PLATIdleLayer");
            }
        }     
    }//end procedure

    public void ActivatePLATLayer(string layerName)
    {//This procedure gives a weighting to each animation and therefore can be activated and managed
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);

    }//end procedure

    void FixedUpdate() //called a fixed amount of times per second
    {
        if(isClimbing == true)
        {
            crouch = false;
        }      
         //move platformer character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);      //TESTER
        jump = false;
        //Raycast is created to be able to detect a ladder
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        Debug.DrawRay(transform.position, Vector2.up, Color.red, distance);
        //Debugging tool used in order to see the ray in runtime

        if (hitInfo.collider != null)
        {                        
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
            rb.useGravity = false;
            animator.SetFloat("PLATSpeedUp", inputVertical);
            //If the player is climbing up the ladder, gravity is set to 0 to prevent the player falling down the ladder
            //The player would then be able to travel vertically whilst on the ladder
        }
        else
        {
            ladderPlatform.SetActive(true);
            hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
            rb.useGravity = true;
            preventJump = false;
            //When the player leaves the ladder, the ladder platform will be restored allowing the player to stand on top of the ladder
            //The gravity scale would then be applied again
        }           
         
    }
    void OnCollisionEnter(Collision col) 
    {     
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyBullet"))
        {//If the player collides with an enemy...

           
            knockbackCount = knockbackLength;
            if(col.transform.position.x < transform.position.x)
            {
                knockFromRight = true;

            }
            else
            {
                knockFromRight = false;
            }
            //The player would be knocked back in the opposite direction they are facing
            if (!(col.contacts[0].otherCollider.transform.gameObject.name == "ShieldPivot"))//If the collision is not with the child object of player called ShieldPivot...
            {//if the player does not block the enemy in time...
                if (EnemyBullet.BulletDamageMultiplierGlobal == 0)
                {
                    EnemyBullet.BulletDamageMultiplierGlobal = 1;
                    //The player would take damage depending on the enemies multiplier
                }

                

                health.MyCurrentValue -= 5 * EnemyBullet.BulletDamageMultiplierGlobal;
                Instantiate(blood, transform.position, Quaternion.identity);
                //The player's health is deducted by 5*multiplier of enemy

            }
            else
            {
                EnemyBullet.BulletDamageMultiplierGlobal = 0;
            }

            // StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
            StartCoroutine(cameraShake.ShakeCustom(0.2f, 1.2f));
            //This coroutine will be responsible for causing a camera shake when the player gets hit by enemy
        }
        if (col.gameObject.tag.Equals("Spikes"))
        {

            Instantiate(blood, transform.position, Quaternion.identity);
            health.MyCurrentValue -= 10;
            
                StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
            
         //If the player lands on a "spikes" they will also take damage

        }
    }
   public void PlayerHitByBoss()//FOR BOSS AI
    {  
       // if (collision.gameObject.tag.Equals("Enemy"))
        //{       
            if (DmgTaken == false)
            {//If the player collides with the boss whilst its tag is "Enemy" and damage taken is false.
                Instantiate(blood, transform.position, Quaternion.identity);

                health.MyCurrentValue -= 75;
                
                    StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
                 
                rb.velocity = new Vector2(-150, 0);
                DmgTaken = true;
            //The player would take damage, and damage taken is set to true to allow player to escape trigger
           
        }
           
       // }       
    }
   void OnTriggerExit2D(Collider2D collision)
    {
        DmgTaken = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if ( collision.CompareTag("Swing"))
        {
            SwingRope = collision.gameObject;

            ThisRope = SwingRope.GetComponentInParent<Pendulum>();//.GetChildWithComponent<Pendulum>();
            //This gets an instance of the object that contains the Pendulum script
            if (collision.CompareTag("Swing"))
            {
                ThisRope.Swinging = true;
            }
        }
       // else if (collision.CompareTag("Swing"))
        //{//If the player is in range of a swing rope

       //     ThisRope.Swinging = true;
            //The player would therefore be able to swing on the rope
      //  }
        else
        {
            Debug.Log("Swing rope: " + SwingRope);
            if (ThisRope != null)
            {
                ThisRope.Swinging = false;
            }
        }

        
    }


}//end class
