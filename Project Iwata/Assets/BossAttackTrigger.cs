using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{//NON ACTIVE SCRIPT
 //This class manages enemy boss' CLOSE ATTACK animation between switching states.

    public Transform attackPoint;//used globally in BossChargeBehaviour also
    public float attackRange = 0.5f;
    public LayerMask TargetLayers;

    public bool test = false;
    public GameObject BossObject;
    public Animator animator;
    public GameObject SummonMinion;
    public Rigidbody2D rb; //Assigned to player rigid body in unity
    //assigned publically to be used with multiple classes

    BoxCollider2D HitRange;
    bool PlayerGetsHit = false;
    Vector3 SummonPosition;

    [SerializeField]
    private Stat PlayerHealth;
    private PlatformPlayerMovement PlayerTakeDamage;
    //Serialize field is used in order to see private variables within the Unity engine inspector
  
    // Start is called before the first frame update
    public void Start()
    {
        attackPoint= GameObject.Find("AttackPoint").transform;
        PlayerTakeDamage = GameObject.FindObjectOfType<PlatformPlayerMovement>();
        HitRange = animator.GetComponentInChildren<BoxCollider2D>();
        //In Unity, a game objects is able to have a heirarchy.
        //Therefore HitRange is assigned to a component within the Boss object (its hit box)
    }
   
    void OnTriggerEnter2D(Collider2D collision)
    {//This procedure is triggered when a collision between 2 hitboxes occurs (Player/bullet with enemy)

        if (collision.gameObject.CompareTag("Player") && PlayerGetsHit != true) 
        {//If the collision with the boss' was a collision that was tagged "Player" ie the player...
            animator.SetBool("InMeleeAttackRange", true);
            PlayerGetsHit = true;
            //if player remains within the area which causes this trigger, they will take damage
            //The boss animation state will also change to a close/melee attack animation, that spawns smaller enemies   
        }
        else
        {//if the collision was not between the player and boss...
            PlayerGetsHit = false;
            //Player does not take damage
            Debug.Log("Disabled hit"); 
            //Debug.log allows you to see in console whether this code is being executed correctly
        }
 
    }//end procedure
   
    void OnTriggerExit2D(Collider2D collision)//for the boss
    {//This procedure is activated when an object leaves the bosses hit box.
        BossObject.tag = "NPC(Non interactable)";
        animator.SetBool("InMeleeAttackRange", false);
        PlayerGetsHit = false;
        //The player cannot take damage when it has left the trigger.
        //bosses animation close attack state is set to false and therefore returns to default.
        
        //The boss tag is assigned to NPC (Therefore non hostile but still interactable)
        

    }//end procedure

    public void OnEnemyAttack()
    {//This function is activated INBETWEEN an animation (being the attack animation)

        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, TargetLayers);
        //Creates a circle collider which will detect if the player enters, of an assigned range and point

        foreach(Collider2D enemy in hitEnemies)
        {//foreach used in order to call function from PlatformPlayerScript whenever the player a target enters the circle collider
            Debug.Log("Enemy Boss hit " + enemy.name);
            PlayerTakeDamage.PlayerHitByBoss();
        }
        

    }//end procedure

    public void OnEnemyFinishAttack()
    {//This function is activated  END an animation (being the attack animation)
        //A minion will then also be summoned once the boss uses its primary attack:

        SummonPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0);
        //At the end of a close range attack a weaker enemy is assigned to a random location on the screen

        Instantiate(SummonMinion, SummonPosition, Quaternion.identity);
        //The enemy object is instantiated

        if (PlayerGetsHit == false)
        {
           BossObject.tag = "NPC(Non interactable)";
       
            //rb.velocity = new Vector2(-10, 10);
            //Switches back to non hostile but still interactable
        }
   
    }//end procedure

    void OnDrawGizmosSelected()
    {//draws the circle collider in order to use for debugging
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
 
}//end class

