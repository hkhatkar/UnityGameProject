using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehaviour : StateMachineBehaviour
{//This class manages enemy bosses switch from idle to moving animation between switching states. (Start of boss battle)
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("WalkToPlayer", true);
            animator.SetBool("ShieldFromPlayer", false);
        }//At the start of the boss battle, the boss will be standing still. When the player walks towards the boss...
        //...(Pressing D key) then the boss will start to walk towards the player location.

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("ShieldFromPlayer", true);
            animator.SetBool("WalkToPlayer", false);
        }//When the player presses the S key, the boss will switch from idle to shielding/retreat state straight away.
    }//end procedure
}
