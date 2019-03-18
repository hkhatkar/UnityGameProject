using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnHit : MonoBehaviour
{

    public GameObject BreakableObject;
    public GameObject BlockParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PlayerBullet"))
        {
           

            Instantiate(BlockParticles, transform.position, Quaternion.identity);

            Destroy(BreakableObject);
            Destroy(col.gameObject);
       
            // force is how forcefully we will push the player away from the enemy.



        }
       


        }


    }

