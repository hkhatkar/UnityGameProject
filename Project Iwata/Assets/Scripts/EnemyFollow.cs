using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{//EnemyFollow class is responsible for enemy type 1 and boss enemy so that the player can be tracked and chased
    public float FollowSpeed= 7;
    private Transform target;
    public float crawlDistance;
    public float chaseDistance;
    public Animator Enemy_animator;
    GameObject EnemyObject; //THIS IS NEEDED, REFERENCES WHOLE ENEMY NOT JUST THE FOLLOW SCRIPT INSIDE IT

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

        if (StunEnemy == true)
        {
            FollowSpeed = 0;
            StartCoroutine(ExecuteAfterTime(1));
        }

        else if (Vector3.Distance(EnemyObject.transform.position, target.position) > crawlDistance)
        {//if the distance from the enemy to player is more than the distance that the enemy is required to stop
            FollowSpeed = 0;
            EnemyObject.transform.position = Vector3.MoveTowards(transform.position, target.position, FollowSpeed * Time.deltaTime);
            //assign enemy object a vector 2 movement that will follow the target to its position
        }
        else if (Vector3.Distance(EnemyObject.transform.position, target.position) < chaseDistance)
        {
            FollowSpeed = 3;
            EnemyObject.transform.position = Vector3.MoveTowards(transform.position, target.position, FollowSpeed * 3 * Time.deltaTime);
        }
        else
        {
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
        Debug.Log(col + "yeyeyeyeyeyeyeyeyeyeyeyeyeyeyeyeyeyeyeyeeyeyeyyeyeyeyeyeyeye");
        //Debug.Log(col.gameObject);
        if (col.gameObject.CompareTag("Player"))

        {
            Debug.Log("owwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
            // FollowSpeed = 0;
            StunEnemy = true;
            // StartCoroutine(ExecuteAfterTime(1));



        }
        
    
       
       
    }
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
       {
            //  FollowSpeed = 0;

            // StartCoroutine(ExecuteAfterTime2(1));
            // FollowSpeed = 3;
          //  if (knockFromRight)
          //  {//If the player is hit by an enemy from the right direction
          //      rb.velocity = new Vector2(2, 0);//knock player back at opposite direction
           // }
           //  if (!knockFromRight)
          //  {//If the player is hit by an enemy from the leftdirection
          //      rb.velocity = new Vector2(-2, 0);//knock player back at opposite direction
          //  }
        }
        
         //   FollowSpeed = 3;
        
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
      //      FollowSpeed = 3;
        }
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        //FollowSpeed = 0;
        yield return new WaitForSeconds(time);     
        //FollowSpeed = 3;
        StunEnemy = false;
    }
  //  IEnumerator ExecuteAfterTime2(float time)
  //  {
        //FollowSpeed = 0;
   //     yield return new WaitForSeconds(time);
   //     FollowSpeed = 3;
   // }

}//end class
