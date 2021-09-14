using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{//This class is responsible for the rotation of the player shield
    public float speed = 5f;
  //  public static bool Blocked = false;

    // Update is called once per frame
    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
        //The shield will revolve around the player and will face the direction that the player aims with the mouse
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

          if (collision.CompareTag("Enemy"))
            {//If the shield collides with the enemy, Blocked = true
             //   Blocked = true;
            }
            else
            {//else blocked = false and the player takes damage
             //   Blocked = false;
            }    
    }//end procedure
}//end class
