using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float FollowSpeed;
    private Transform target;
    public float stoppingDistance;
    public Animator Enemy_animator;
    public GameObject EnemyObject;
   // public float health;
    public GameObject PlatBullet;
   // public GameObject blood;

    


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      //  if(health <= 0)
      //  {
           //Debug.Log("Dead");
     //       Destroy(EnemyObject);
     //   }
        if (Vector2.Distance(EnemyObject.transform.position, target.position) > stoppingDistance)
        {
            EnemyObject.transform.position = Vector2.MoveTowards(transform.position, target.position, FollowSpeed * Time.deltaTime);
        }

        if (target.position.x > 0)
        {
            Enemy_animator.SetBool("PLATEnemyx", true);
        }
        else
        {
            Enemy_animator.SetBool("PLATEnemyx", false);
        }
    }

   // void OnTriggerEnter2D(Collider2D bulletcol)
   // {
       // Debug.Log("Ind");
     //   if(bulletcol.CompareTag("PlayerBullet"))
     //   {
            // Debug.Log("Hit");
     //       if (Bullet.SwitchShot == true)
     //       {
     //           health -= 0.1f;

     //       }
      //      else if (Bullet.SwitchShot == false)
        //    {

        //        health -= 1;

        //    }
            
        //    Instantiate(blood, transform.position, Quaternion.identity);
           
        //   Destroy(bulletcol.gameObject);
      //  }
        
  //  }

}
