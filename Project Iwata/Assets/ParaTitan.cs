using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaTitan : MonoBehaviour
{
    public GameObject GroundProjectile;
    public Transform BashPoint;
    public float BashLifetime;
    public float ProjectileSpeed;
    public int health = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        
       
        
        InvokeRepeating("GroundBash", BashLifetime, ProjectileSpeed);
       // StartCoroutine(GroundBash());

       

    }

    // Update is called once per frame
    void Update()
    {
       if(health == 0)
        {
            Destroy(gameObject);
        }
    }

    void GroundBash ()
    {
    //    yield return new WaitForSeconds(3);
        Instantiate(GroundProjectile, BashPoint.position, transform.rotation);
       
    }
    void OnTriggerEnter2D(Collider2D bulletcol)
    {
        // Debug.Log("Ind");
        if (bulletcol.CompareTag("PlayerBullet"))
        {
            // Debug.Log("Hit");
            health -= 1;
            Destroy(bulletcol.gameObject);
        }

    }
}
