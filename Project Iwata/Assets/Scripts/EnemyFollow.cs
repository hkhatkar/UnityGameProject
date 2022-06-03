using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{//EnemyFollow class is responsible the enemy behaviour that the player is tracked and chased
    public float FollowSpeed= 7;
    private Transform target;
    public float crawlDistance;
    public float chaseDistance;
    public Animator Enemy_animator;

    GameObject EnemyObject; //References the whole enemy not just the scripts inside it 

    public GameObject PlatBullet;
    public bool StunEnemy = false;
   
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        EnemyObject = gameObject.transform.gameObject; //was parent

        //target is assigned to the object tagged "Player" (which is the player)
    }

    // Update is called once per frame
    void Update()
    {

        //Enemy could either be within crawl distance, chase distance, our outside both

        if (StunEnemy == true)
        {//If the enemy is stunned its follow speed is 0 for a short while. This happens when the player gets hit by this enemy
            FollowSpeed = 0;
            StartCoroutine(ExecuteAfterTime(1));
            
        }
        else if (Vector3.Distance(EnemyObject.transform.position, target.position) > crawlDistance)
        {//if the distance from the enemy to player is more than (outside) the distance that the enemy is required to follow
            FollowSpeed = 0;
            EnemyObject.transform.position = Vector3.MoveTowards(transform.position, target.position, FollowSpeed * Time.deltaTime);
            //assign enemy object a vector 3 movement that will follow the target to its position
            //Sets the follow speed to = 
        }
        else if (Vector3.Distance(EnemyObject.transform.position, target.position) < chaseDistance)
        {//If the enemy is within chase distance, it will follow the player at its maximum possible speed
            FollowSpeed = 3;
            EnemyObject.transform.position = Vector3.MoveTowards(transform.position, target.position, FollowSpeed * 3 * Time.deltaTime);
        }
        else
        {//else if the enemy is within crawl distance, the enemy would move towards the player at a reasonable but slower speed
            FollowSpeed = 3;
            EnemyObject.transform.position = Vector3.MoveTowards(transform.position, target.position, FollowSpeed * Time.deltaTime);
        }
        
        

        if (target.position.x > 0)
        {//if the targets position is more than 0, the enemy animator will display the left facing animation for enemy
            Enemy_animator.SetBool("PLATEnemyx", true);
        }
        else
        {//else the enemy animator will display the right facing animation for enemy
            Enemy_animator.SetBool("PLATEnemyx", false);
        }
    }
    

    public void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject);
        if (col.gameObject.CompareTag("Player"))
        {//enemy is stunned if it collides with player
            StunEnemy = true;
        } 
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {//co routine to set enemy stun back to false after a certain amount of time
        yield return new WaitForSeconds(time);     
        StunEnemy = false;
    }

}//end class
