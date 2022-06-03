using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeBehaviour : StateMachineBehaviour
{//NON ACTIVE SCRIPT
//This class manages enemy bosses CHARGE animation between switching states (when player attempts to attack enemy for too long).
    
    PlatformPlayerMovement PlayerTakeDamage;
    public float speed;
    public float distance;
    private bool movingLeft = true;
    //Declares variables for the direction player is facing
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {//This is the initial direction of the bosses charge attack

        PlayerTakeDamage = GameObject.FindObjectOfType<PlatformPlayerMovement>();


        if (FlipEnemyDirection.FacingLeft == true)
        {//if enemy is facing left...
            movingLeft = true;
            animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
            //The animator will flip the animation to face the player left.

        }
        else
        {
            movingLeft = false;
            animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
            //The animator will flip the animation to face the player right.
        }
    }//end procedure

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (FlipEnemyDirection.FacingLeft == true)
        {
           if( movingLeft == true)
            {//If player suddenly moves to the left of the boss whilst charging
                animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
                

            }//The boss object will flip direction again to face the player


        }
        else if (FlipEnemyDirection.FacingLeft == false)
        {//If player suddenly moves to the right of the boss whilst charging
            if (movingLeft == false)
            {
                animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }//The boss object will flip direction again to face the player.

        }
        
    
    }//end class 

    
}//end procedure
