using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIAnimatorManager : StateMachineBehaviour
{//NON ACTIVE SCRIPT
    //This class manages only the basic enemy boss' animation between switching states.
 //This class is only for the boss non-attacking states
 
    // OnStateUpdate is called on each Update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {//If player presses space bar...
            animator.SetBool("WalkToPlayer", true);
            animator.SetBool("ShieldFromPlayer", false);
            //The enemy animator will set the Walk to player state as true and disable enemies shield state
        }//end if
        if (Input.GetKeyDown(KeyCode.S))
        {//if player presses S...
            animator.SetBool("ShieldFromPlayer", true);
            animator.SetBool("WalkToPlayer", false);
            //The enemy boss would hold up his shield animation 
            //This means that ShieldFromPlayer state is set to true and WalkToPlayer state will be disabled
        }
    }//end State procedure
    
}//end class
