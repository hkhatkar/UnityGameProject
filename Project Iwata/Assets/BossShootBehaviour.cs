using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBehaviour : StateMachineBehaviour
{//This class manages enemy bosses LONG RANGE ATTACK animation between switching states. (Start of boss battle)

    public GameObject FireBall;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
            Instantiate(FireBall, animator.transform.position, animator.transform.rotation);
            //Instantiates a fireball attack that acts the same as player's bullet however is activated when player moves far from the boss
    }//end procedure
}//end class
