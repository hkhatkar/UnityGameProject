using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject EnemyObject;
    public float health;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //Debug.Log("Dead");
            Destroy(EnemyObject);
        }
    }
    void OnTriggerEnter2D(Collider2D bulletcol)
    {
        // Debug.Log("Ind");
        if (bulletcol.CompareTag("PlayerBullet"))
        {
            // Debug.Log("Hit");
            if (Bullet.SwitchShot == true)
            {
                health -= 0.1f;

            }
            else if (Bullet.SwitchShot == false)
            {

                health -= 1;

            }

            Instantiate(blood, transform.position, Quaternion.identity);

            Destroy(bulletcol.gameObject);
        }

    }
}
