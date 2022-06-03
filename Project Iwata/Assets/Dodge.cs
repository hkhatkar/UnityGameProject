using System.Collections;
using System.Collections.Generic;   //use lerp to prevent jerking
using UnityEngine;

public class Dodge : MonoBehaviour
{//Dodge class is responsible for carrying out the dodge feature which gives the user a temporary burst of speed

   private bool CannotDodge;
    static public bool playDodgeAnim = false;
    public Animator animator;
    private Rigidbody rb;
    public float DodgeSpeed;
    private float DodgeTime;
    public float startDodgeTime;
    private int direction;
    public int CooldownTime;
    private float nextUseTime = 0;

    NewPLATPlayerMovement PlayerMoveScript;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMoveScript = gameObject.GetComponent<NewPLATPlayerMovement>();
        rb = GetComponent<Rigidbody>();
        DodgeTime = startDodgeTime;
    
    }
    public void checkIfInAir(bool grounded)//Checks by passing Grounded from Character controller class
    {
        CannotDodge = grounded;
    }


    void FixedUpdate()
    {// Update is called once per frame

        if (direction == 0)
        //Didnt put Shift in above if statement because if the player is walking before pressing Shift, dodge will not happen
        {
            AbilityManager.AbilityInUse = false;
            playDodgeAnim = false;
            if (Time.time > nextUseTime)//delta time? //nextUseTime
            {//if the time of the dodge is more than the maximum time...
              
                if (animator.GetBool("PLATx") == true && Input.GetKey(KeyCode.LeftShift)) // left
                {//If the player is facing left (PLATX = true) and left shift is pressed...

                    
                    
                    direction = 1;
                    //the direction is assigned to 1
                    
                }
                else if (animator.GetBool("PLATx") == false && Input.GetKey(KeyCode.LeftShift)) // right
                {//If the player is facing right (PLATX = false) and left shift is pressed...
                    direction = 2;
                    //direction is assigned to 2
                }
            }
        }

        else
        {
            if(DodgeTime <= 0)
            {//if the duration of the dodge is assigned to 0 or less in Unity, the velocity will be set to 0
              //  gameObject.GetComponent<Renderer>().material.color= NormalColor;
                direction = 0;
                DodgeTime = startDodgeTime;
                animator.SetBool("PLATDodge", false);
                rb.velocity = Vector3.zero;
            }
            else
            {//If the dodge time is more than 0...
                DodgeTime -= Time.deltaTime;
                //Dodge time is decremented by time until it reaches 0
                animator.SetBool("PLATDodge", true);
                
                if (direction == 2)
                {//if the direction is right
                    AbilityManager.AbilityInUse = true;
                    playDodgeAnim = true;
                    animator.Play("Dodge");
                    nextUseTime = Time.time + CooldownTime;
                    PlayerMoveScript.HandleDodge(16f);

                    //Velocity acts on the right direction multiplied by the speed variable
                    //nextUseTime acts as a cooldown so the player would have to wait until this cooldown is finished before using again
                }
                else if (direction == 1)
                {
                    AbilityManager.AbilityInUse = true; 
                    playDodgeAnim = true;
                    animator.Play("DodgeBack");
                    nextUseTime = Time.time + CooldownTime;
                    PlayerMoveScript.HandleDodge(-16f);
                    //Velocity acts on the left direction multiplied by the speed variable
                    // nextUseTime acts as a cooldown so the player would have to wait until this cooldown is finished before using again
                }
            }
        }
        
    }//end procedure

}//end class
