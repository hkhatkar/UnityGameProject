using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemyDirection : MonoBehaviour
{//NON ACTIVE SCRIPT
//This class is used to flip the enemy bosses direction to face the direction the player is 

    float EnemyX;
    float PlayerX;
    public Transform PlayerPos;

    public static bool FacingLeft = true; 
    //used in BossChargeBehaviour for determining charge direction

    void Update()
    {// Update is called once per frame

        EnemyX = transform.position.x;
        Debug.Log("X1 equals " + EnemyX);
        //Debug log is used to display the X coord of the enemy in console
      
        PlayerX = PlayerPos.transform.position.x;
        Debug.Log("X2 equals " + PlayerX);
        //Debug log is used to display the X coord of the player in console, used when debugging
        if (FacingLeft == true)
        {//if the enemy is facing left...
            if (PlayerX > EnemyX)
            {//and if the players x value is larger than the enemies (Player on right of enemy)
                Vector2 theScale = transform.localScale;

                theScale.x *= -1;
                //The x scale for the enemy boss is flipped to face player (multipled)
                transform.localScale = theScale;
                FacingLeft = false;
            }
        }
        else if  (FacingLeft == false)
        {//else if the enemy is facing towards right
            if (PlayerX < EnemyX)
            {//and the player's x value is smaller than enemies (player is left of enemy)
                Vector2 theScale = transform.localScale;

                theScale.x /= -1;
                //The enemy is flipped to face the left by dividing by scale -1

                transform.localScale = theScale;
                FacingLeft = true;
            }
        }

        
    }
    
    
}//end class
