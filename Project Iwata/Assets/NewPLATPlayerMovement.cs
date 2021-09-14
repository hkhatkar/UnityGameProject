using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPLATPlayerMovement : MonoBehaviour
{
    float passedwallJumpVal;
    WallJump WJScript;

    public CharacterController controller;
    bool controllerGrounded;
    Vector3 currentMovement;
    public float MoveSpeed;
    public float gravity;

    public float jumpPower;
    float currentJumpPower;
    int jumpsPerformed = 0;
    bool running = false;
    float h;

    public Animator anim;
    public Transform midEdgePosition;
    
    public Rigidbody rb;
    public bool holdingLedge = false;
    Vector3 ledgeCornerPoint = Vector3.zero;
    public bool crouch = false;

    int wallJumpImpact = 0; //hererererererere
    int dirOfWallKnockback = 0;


    int dirOfDodge;
    float passedDodgeSpeed;

    public bool SprintCoolDown = false;
  

    // Start is called before the first frame update
    void Start()
    {
        
        WJScript = gameObject.GetComponent<WallJump>();
        passedwallJumpVal = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //passedwallJumpVal = WJScript.currentWallJumpPower;

        if (controller)
        {
            handleMovement();
            controllerGrounded = controller.isGrounded;
            handleAnimation();
            handleLedgeGrab();
            handleShooting();
        }

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void handleMovement()
    {
        //  float v = Input.GetAxis("Vertical");
        

        h = Input.GetAxis("Horizontal"); // walking
            if (holdingLedge)
        {
            jumpsPerformed = 1; //fixes holding ledge jump, when jumped 2 times already, the jump off ledge doesnt work
        }
            if (controller.isGrounded)
            {
          
            currentMovement.y = 0;
            jumpsPerformed = 0;
                currentMovement = new Vector3(h, 0, 0);

            
             if (running == false && jumpsPerformed != 2) { MoveSpeed = 4f; }
            }
            currentMovement = new Vector3(h, 0, 0);
            currentMovement *= MoveSpeed;
            currentMovement.y -= gravity;
            controller.Move(currentMovement * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && jumpsPerformed<2)
            {
                if (jumpsPerformed == 1)
                {
                passedwallJumpVal = 0;
                    MoveSpeed = 7f;
                    currentJumpPower = 28f;
                }
             
                else { currentJumpPower = jumpPower; }
                jumpsPerformed++;
            }
            if (Input.GetKey(KeyCode.LeftControl) && !SprintCoolDown && controller.isGrounded)
            {
           

                if (anim.GetBool("PLATSprinting") == false)
                {
                  if (anim.GetBool("PLATx") == true) { anim.Play("PLATSprintLeftStart"); }
                    else { anim.Play("PLATSprintRightStart"); }
                }
                    anim.SetBool("PLATSprinting", true);

                     MoveSpeed = 7f;
                     running = true;
             }
            else if (SprintCoolDown == true)
            {
            MoveSpeed = 4f;
             }   
            if (Input.GetKeyUp(KeyCode.LeftControl) || !controller.isGrounded)
             {
                anim.SetBool("PLATSprinting", false);
                 running = false;

            }

//---------------------------------------------------------------------WallJump
        if (passedwallJumpVal > 0 && dirOfWallKnockback > 0)
        {
            passedwallJumpVal -= 20 * Time.deltaTime;
            currentMovement.x = passedwallJumpVal;
        }
        else if (passedwallJumpVal < 0 && dirOfWallKnockback < 0)
        {
            passedwallJumpVal += 20 * Time.deltaTime;
            currentMovement.x = passedwallJumpVal;
        }
//---------------------------------------------------------------------Dodge
        if (passedDodgeSpeed > 0 && dirOfDodge > 0)
        {
            passedDodgeSpeed -= 40 * Time.deltaTime;
            currentMovement.x = passedDodgeSpeed;
        }
        else if (passedDodgeSpeed < 0 && dirOfDodge < 0)
        {
            passedDodgeSpeed += 40 * Time.deltaTime;
            currentMovement.x = passedDodgeSpeed;
        }
//---------------------------------------------------------------------

        //else if (passedwallJumpVal > 0 && anim.GetBool("PLATx") == true)
        // {
        //     passedwallJumpVal += 23 * Time.deltaTime;
        //      currentMovement.x = passedwallJumpVal;
        // }
        // if (!controller.isGrounded)
        // {
        currentJumpPower -= gravity * 3 * Time.deltaTime;

           
         //   currentDodgePower -= 3 * Time.deltaTime;
        //}
           
        //if (currentJumpPower > 0)
      //  {
            currentMovement.y = currentJumpPower;
       // currentMovement.x = (currentDodgePower + passedwallJumpVal) * MoveSpeed;
           // currentMovement.x = currentDodgePower;
       // }
      //  else
       // {
        //    currentMovement.y = 0;
       // }
          controller.Move(currentMovement * Time.deltaTime);
        
    }
   //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void handleAnimation()
    {
            anim.SetFloat("PLATSpeed", Mathf.Abs(h));
            if (Input.GetButton("Jump") && jumpsPerformed > 1) { anim.SetBool("PLATDoubleJump", true); anim.SetBool("PLATJump", false); }

            else if (Input.GetButton("Jump")) { anim.SetBool("PLATJump", true); }

        // if (crouch){ActivatePLATLayer("PLATCrouchLayer"); }
        if (crouch) { ActivatePLATLayer("PLATCrouchLayer"); }
        else
        {
            ActivatePLATLayer("PLATIdleLayer");
        }

        if (controllerGrounded == true) {
            anim.SetBool("PLATJump", false); anim.SetBool("PLATDoubleJump", false);

        }
            if (Input.GetKeyDown(KeyCode.A)) { anim.SetBool("PLATx", true); }
            if (Input.GetKeyDown(KeyCode.D)) { anim.SetBool("PLATx", false); }

            if (Dodge.playDodgeAnim == true)
            {//dodge animation played when dodging

                //   ActivatePLATLayer("PLATDodgeLayer");
            }
            else
            {//idle animation played on default
             // ActivatePLATLayer("PLATIdleLayer");
            }

            //ActivatePLATLayer("PLATIdleLayer");
        
    }
    void ActivatePLATLayer(string layerName)
    {//This procedure gives a weighting to each animation and therefore can be activated and managed
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(anim.GetLayerIndex(layerName), 1);

    }//end procedure
    void switchBackToJumpAnim()//from double jump to normal jump anim (EVENT)
    {
        anim.SetBool("PLATDodge", false);
        anim.SetBool("PLATDoubleJump", false);
       // if (anim.GetBool("PLATDodge") == true)
     //   {
            anim.SetBool("PLATJump", true);
       // }
      //  else
     //   {
     //       anim.SetBool("PLATJump", false);
     //   }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void handleLedgeGrab()
    {
        RaycastHit DownEdgeRange, LowerEdgeRange, UpperEdgeRange;
        int facingRightMultiplier = -1; //switches direction of raycast

        if (anim.GetBool("PLATx") == true) //if facing left
        {
            facingRightMultiplier = 1;
        }

        if (//Physics.Raycast(midEdgePosition.position+ new Vector3(0,0.6f,0), Vector3.left, out UpperEdgeRange) ==false &&
            Physics.Raycast(midEdgePosition.position + new Vector3(0, 0.2f, 0), Vector3.left * 2 * facingRightMultiplier, out LowerEdgeRange, 10) &&
            Physics.Raycast(midEdgePosition.position + new Vector3(-1f * facingRightMultiplier, 1f, 0), Vector3.down, out DownEdgeRange, 10) &&
                Physics.Raycast(midEdgePosition.position + new Vector3(0f, 0.7f, 0), Vector3.left * 2 * facingRightMultiplier, out UpperEdgeRange, 10))
        {
            Debug.Log("ooo");
            if (DownEdgeRange.collider.tag == "Finish" && LowerEdgeRange.collider.tag == "Finish" && UpperEdgeRange.collider.tag == "Finish")
            {
                //mid
                holdingLedge = true;
                controller.enabled = false;
                rb.isKinematic = true;
                //  currentJumpPower = 0;
                //  holdingLedge = true;
                //  gravity = 0f;
                ledgeCornerPoint = new Vector3(LowerEdgeRange.point.x, DownEdgeRange.point.y, transform.position.z);
                //vvvvv THESE ARE ONLY FOR RIGHT HANG, LEFT HANG WOULD BE -0.43 I THINK)
                transform.position = ledgeCornerPoint + new Vector3(0.43f * facingRightMultiplier, -0.37f, 0);

                Debug.Log("LCP: " + ledgeCornerPoint);
                //   holdingLedge = true;
                // currentMovement = Vector3.zero;
                // controller.Move(currentMovement* Time.deltaTime);

            }
            else
            {
                //   holdingLedge = false;
                //currentJumpPower = 0;
                //  gravity = 14f;
            }

        }
        else
        {
            // holdingLedge = false;
            //currentJumpPower = 0;
            //  gravity = 14f;
        }

        if (holdingLedge == true && (
             Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))//left only
        {
            if (facingRightMultiplier == 1)
            {
                anim.Play("DodgeBack");
            }
            else
            {
                anim.Play("Dodge");
            }
            rb.isKinematic = false;
            controller.enabled = true;
            holdingLedge = false;
            transform.position = ledgeCornerPoint + new Vector3(-0.1f * facingRightMultiplier, 1f, 0);

        }
        else if (holdingLedge == true && (Input.GetKeyDown(KeyCode.S)))

        {

            if (facingRightMultiplier == 1)
            {
                anim.Play("PLATJumpBack");
            }
            else
            {
                anim.Play("PLATJump");
            }
            controller.enabled = true;
            rb.isKinematic = false;
            holdingLedge = false;
            transform.position = ledgeCornerPoint + new Vector3(2f * facingRightMultiplier, 0f, 0);
        }


    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void handleShooting()
    {
        //if (jump == false)
        // {
        if (Input.GetMouseButtonDown(0)) //"Crouch"
        {
            crouch = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            crouch = false;
        }
        // }

    }

    private void OnDrawGizmos()
    {
        //Ledge detection
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(midEdgePosition.position + new Vector3(-1f, 1f, 0), Vector3.down);
        Gizmos.DrawRay(midEdgePosition.position + new Vector3(0, 0.2f, 0), Vector3.left);
        Gizmos.DrawRay(midEdgePosition.position + new Vector3(0, 0.7f, 0), Vector3.left);
    }

   //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   public void HandleWallJump(float maxPassedWJPower)
    {
        Debug.Log("WallJumpHandled " + maxPassedWJPower);
        //if (anim.GetBool("PLATx") == true)
        // {
        if (anim.GetBool("PLATx") == true)
        {
            anim.SetBool("PLATx", false);
        }
        else 
        {
            anim.SetBool("PLATx", true);
        }
        dirOfWallKnockback = (int) maxPassedWJPower; //this doesnt change unless another wall jump is executed
            passedwallJumpVal = maxPassedWJPower;
      //  }
      //  else
     //   {
    //        passedwallJumpVal = -maxPassedWJPower;
     //   }
       jumpsPerformed = 2;
        currentJumpPower = 30;
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void HandleDodge(float DodgeSpeed)
    {

        dirOfDodge = (int)DodgeSpeed;
        passedDodgeSpeed = DodgeSpeed;
    }

    public void KnockbackManager()
    {

    }
}
