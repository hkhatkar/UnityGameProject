using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemyDirection : MonoBehaviour
{

    float EnemyX;
    float PlayerX;
    public Transform PlayerPos;
    bool FacingLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {

        EnemyX = transform.position.x;
        Debug.Log("X1 equals " + EnemyX);
      
        PlayerX = PlayerPos.transform.position.x;
        Debug.Log("X2 equals " + PlayerX);
        if (FacingLeft == true)
        {
            if (PlayerX > EnemyX)
            {
                Vector2 theScale = transform.localScale;

                theScale.x *= -1;

                transform.localScale = theScale;
                FacingLeft = false;
            }
        }
        else if  (FacingLeft == false)
        {
            if (PlayerX < EnemyX)
            {
                Vector2 theScale = transform.localScale;

                theScale.x /= -1;

                transform.localScale = theScale;
                FacingLeft = true;
            }
        }

        
    }

    // Update is called once per frame
    
}
