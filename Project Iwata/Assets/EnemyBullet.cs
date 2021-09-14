using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{//This is responsible for the bullets fired by enemy type 2, covering where it aims and the bullet itself
    float moveSpeed = 7f;
    Rigidbody2D rb;

    public static int BulletDamageMultiplierGlobal; 
    //changes for different types of bullet damage for a weapon. This is also used across other scripts (Global)

    PlatformPlayerMovement target;

    Vector2 moveDirection;
    private int BulletDamageMultiplierLocal;
    //this isn't used across other scripts and is passed to the global version

    // Start is called before the first frame update
    void Start()
    {
        BulletDamageMultiplierGlobal = BulletDamageMultiplierLocal;
        //Sets the global bullet multiplier as the local one for this script
        rb = GetComponent<Rigidbody2D> ();
        target = GameObject.FindObjectOfType<PlatformPlayerMovement>();
        //target is assigned to the object that contains the script PlatformPlayerMovement (the player)
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        //This moves the enemy to face in the direction of the player
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
        //The bullet is destroyed in 3 seconds
        
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {//if the bullet collides with the player
            
            Debug.Log("Hit");
            //Debug.log allows you to see if the player gets hit by the bullet in the console
            Destroy(gameObject, 0.01f);
            //The bullet is destroyed instantly
            //player health is covered separately in another class
        }
    }//end procedure
}//end class
