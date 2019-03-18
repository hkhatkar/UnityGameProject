using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCautiousWalkBehaviour : StateMachineBehaviour
{

    public float speed;//speed
    public float stoppingDistance;//enemy stops getting closer
    public float retreatDistance;//when the enemy backs away

    private Transform player;

    private Vector2 Pos;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Pos = animator.transform.position;
        
        // animator.gravityScale = 1000; //CHANGE LATER??????????????????????????? PREVENTS SKELE FROM MOVING UP AND DOWN ONLY SIDE TO SIDE
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pos.y = -4;//NOT THE MOST EFFICIENT WAY OF MAKING  Y POSITION OF ENEMY CONSTANT SO FIX LATER ON
        animator.transform.position = Pos;
        
        if (Vector2.Distance(animator.transform.position, player.position) > stoppingDistance)
        {
            Pos = Vector2.MoveTowards(animator.transform.position, player.position, speed * Time.deltaTime);//towards player

        }
        else if (Vector2.Distance(animator.transform.position, player.position) < stoppingDistance && Vector2.Distance(animator.transform.position, player.position) > retreatDistance)
        {
            Pos = animator.transform.position;//still CONTAINED this.animator
        }
        else if (Vector2.Distance(animator.transform.position, player.position) < retreatDistance)//away from player
        {
            Pos = Vector2.MoveTowards(animator.transform.position, player.position, -speed * Time.deltaTime);
        }
        //animator.transform.position
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
