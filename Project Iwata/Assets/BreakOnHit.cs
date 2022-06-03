using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnHit : MonoBehaviour
{//NON ACTIVE SCRIPT
//This class is used to destroy any breakable object that have collided with a player bullet

    public GameObject BreakableObject;
    public GameObject BlockParticles;
    //Made public in order to assign within unity to use within different classes

    void OnTriggerEnter2D(Collider2D col)
    {//This procedure will be triggered when something collides with the breakable object
        if (col.gameObject.tag.Equals("PlayerBullet"))
        {//If the thing that collides with the object is tagged "PlayerBullet"..,
          
            Instantiate(BlockParticles, transform.position, Quaternion.identity);
            Destroy(BreakableObject);
            Destroy(col.gameObject);
            //Particles would be created to signify it breaking and the object is destroyed   
        }
       
    }//end procedure

}//end class

