using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToPlayerBehaviour : StateMachineBehaviour
{//This class is responsible for controlling the animations and movement of walking state for enemy boss
    private Transform playerPos;
    public float speed;
    private Vector2 Pos;
    //Declares variables

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Pos = animator.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pos.y = -4;//Sets the boss position to floor level
        animator.transform.position = Pos;
        Pos = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        //This moves the boss object towards the player's position
    }
    
}//end class
