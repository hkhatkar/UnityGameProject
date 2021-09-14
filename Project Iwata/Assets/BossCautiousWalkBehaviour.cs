using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCautiousWalkBehaviour : StateMachineBehaviour
{//This class manages enemy boss' SHIELD/RETREAT animation between switching states (when player attempts to attack enemy).

    public float speed;//speed of enemy boss
    public float stoppingDistance;//enemy boss stops getting closer
    public float retreatDistance;//when the enemy boss backs away

    private Transform player;
    private Vector2 Pos;
    //This declares player's position and object
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Pos = animator.transform.position;
        //As soon as state is entered, the player is assigned to a position that could be detected by boss AI
        
       
    }//end state procedure

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pos.y = -4;
        //This makes sure that the enemy is placed on the same y position as the player (The floor position)
        animator.transform.position = Pos;
        
        if (Vector2.Distance(animator.transform.position, player.position) > stoppingDistance)
        {//If the distance from the boss and player is bigger than the distance the enemy will stop at
            Pos = Vector2.MoveTowards(animator.transform.position, player.position, speed * Time.deltaTime);
            //The enemy boss will advance towards the player until it reaches its stopping distance. 
        }
        else if (Vector2.Distance(animator.transform.position, player.position) < stoppingDistance && Vector2.Distance(animator.transform.position, player.position) > retreatDistance)
        {
            Pos = animator.transform.position;
            //When the distance between the player and boss is between the minimum stopping distance and maximum retreat distance, the enemy position is contained
        }
        else if (Vector2.Distance(animator.transform.position, player.position) < retreatDistance)//away from player
        {//If the distance from the boss and player is less than the distance the enemy will retreat to (when player advances towards boss)
            Pos = Vector2.MoveTowards(animator.transform.position, player.position, -speed * Time.deltaTime);
            //The enemy boss will retreat towards the player until it reaches its retreat distance. 

        }
     
    }//end procedure
  
}//end class
