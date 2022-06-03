using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPLATPlayerMovement : MonoBehaviour
{//This script is the Main player movement script for the side scrolling elements of the game.
//This links all animations to different player movement abilities and any other abilities that effect movement
//and also connects all movement abilities to this script

    public float conWalkSpeed;
    public float conRunSpeed;

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

    int wallJumpImpact = 0;
    int dirOfWallKnockback = 0;
    int dirOfDodge;
    float passedDodgeSpeed;

    public bool SprintCoolDown = false;
    bool HookIsActive;
  

    // Start is called before the first frame update
    void Start()
    {
        
        WJScript = gameObject.GetComponent<WallJump>();
        passedwallJumpVal = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller)
        {
            handleMovement();
            controllerGrounded = controller.isGrounded;
            handleAnimation();
            handleLedgeGrab();
            handleShooting();
        }
    }
    //Basic movement method -----------------------------------------------------------------------------------------------------------------------------------------------------------
    void handleMovement()
    {
        if (HookIsActive)
        {
            currentJumpPower = 0f;
        }

        h = Input.GetAxis("Horizontal"); // walking
        if (holdingLedge)
        {
            jumpsPerformed = 1; 
            //fixes holding ledge jump, when jumped 2 times already
        }
        if (controller.isGrounded)
        { 
            currentMovement.y = 0;
            jumpsPerformed = 0;
            currentMovement = new Vector3(h, 0, 0); 
             if (running == false && jumpsPerformed != 2) { MoveSpeed = conWalkSpeed; }
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
                MoveSpeed = conRunSpeed;
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

            MoveSpeed = conRunSpeed;
            running = true;
        }
        else if (SprintCoolDown == true)
        {
            MoveSpeed = conWalkSpeed;
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
//---------------------------------------------------------------------Jump

        currentJumpPower -= gravity * 3 * Time.deltaTime;
        currentMovement.y = currentJumpPower;
        controller.Move(currentMovement * Time.deltaTime);
        
    }
   //these methods are for handling all animations---------------------------------------------------------------------------------------------------------------------------------------------
    void handleAnimation()
    {
        anim.SetFloat("PLATSpeed", Mathf.Abs(h));
        if (Input.GetButton("Jump") && jumpsPerformed > 1) { anim.SetBool("PLATDoubleJump", true); anim.SetBool("PLATJump", false); }
        else if (Input.GetButton("Jump")) { anim.SetBool("PLATJump", true); }

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
        anim.SetBool("PLATJump", true);
    }

    //method for handling edge grabbing------------------------------------------------------------------------------------------------------------------------------------------------
    void handleLedgeGrab()
    {
        RaycastHit DownEdgeRange, LowerEdgeRange, UpperEdgeRange;
        int facingRightMultiplier = -1; //switches direction of raycast

        if (anim.GetBool("PLATx") == true) //if facing left
        {
            facingRightMultiplier = 1;
        }

        if (
            Physics.Raycast(midEdgePosition.position + new Vector3(0, 0.2f, 0), Vector3.left * 2 * facingRightMultiplier, out LowerEdgeRange, 10) &&
            Physics.Raycast(midEdgePosition.position + new Vector3(-1f * facingRightMultiplier, 1f, 0), Vector3.down, out DownEdgeRange, 10) &&
            Physics.Raycast(midEdgePosition.position + new Vector3(0f, 0.7f, 0), Vector3.left * 2 * facingRightMultiplier, out UpperEdgeRange, 10))
        {//3 raycasts are used here.
        //LowerEdgeRange must detect an object of tag "Finish" for the edge grab to happen
        //UpperEdgeRange must be empty, therefore between this lower and upper range, there should be a ledge in between
        //DownEdgeRange is finally used to make sure that we grab the ledge at the right height, when this touches an object of type ledge it will keep the player at this position

            if (DownEdgeRange.collider.tag == "Finish" && LowerEdgeRange.collider.tag == "Finish" && UpperEdgeRange.collider.tag == "Finish")
            {
                //mid
                holdingLedge = true;
                controller.enabled = false;
                rb.isKinematic = true;
               
                ledgeCornerPoint = new Vector3(LowerEdgeRange.point.x, DownEdgeRange.point.y, transform.position.z);

                //This is ONLY FOR RIGHT HANG
                transform.position = ledgeCornerPoint + new Vector3(0.43f * facingRightMultiplier, -0.37f, 0);
                Debug.Log("LCP: " + ledgeCornerPoint);
            }

        }

        if (holdingLedge == true && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        //jumps in the direction of the way you face whilst holding onto the ledge
        {
            if (facingRightMultiplier == 1)
            {
                anim.Play("DodgeBack");
            }//uses dodge animations to get up the ledge
            else
            {
                anim.Play("Dodge");
            }

            rb.isKinematic = false;
            controller.enabled = true;
            holdingLedge = false;
            transform.position = ledgeCornerPoint + new Vector3(-0.1f * facingRightMultiplier, 1f, 0);
            //Enables movement again once the ledge has been jumped off from

        }
        else if (holdingLedge == true && (Input.GetKeyDown(KeyCode.S)))
        //S is used if you want to drop off the ledge instead
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
    //handles shooting-------------------------------------------------------------------------------------------------------------------------------------------------------------
    void handleShooting()
    {
        if (Input.GetMouseButtonDown(0)) 
        {//when this is held down, bullets will automatically be fired in set time intervals
            crouch = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            crouch = false;
        }
    }

    private void OnDrawGizmos()
    {
        //Ledge detection
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(midEdgePosition.position + new Vector3(-1f, 1f, 0), Vector3.down);
        Gizmos.DrawRay(midEdgePosition.position + new Vector3(0, 0.2f, 0), Vector3.left);
        Gizmos.DrawRay(midEdgePosition.position + new Vector3(0, 0.7f, 0), Vector3.left);
    }

   //method for handling wall jump-----------------------------------------------------------------------------------------------------------------------------------------------------------------
   public void HandleWallJump(float maxPassedWJPower)
    {
        Debug.Log("WallJumpHandled " + maxPassedWJPower);
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
        jumpsPerformed = 2;
        currentJumpPower = 30;
    }
    //methos for determining direction of dodge and controlling its velocity---------------------------------------------------------------------------------------------------------------------
    public void HandleDodge(float DodgeSpeed)
    {
        dirOfDodge = (int)DodgeSpeed;
        passedDodgeSpeed = DodgeSpeed;
    }

    public void KnockbackManager()
    {
        //to be implemented when needed
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void HookManager(bool HookActive)
    {
        //theres a bug where gravity gets very strong whilst held up in the air, we need to make it 0
        HookIsActive = HookActive;  
    }
}
