using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{//NON ACTIVE SCRIPT
    //This class is responsible for preventing the player jumping and climbing a ladder at the same time
 //It is also used to control whether the ladder could be climbed by the player

    public GameObject ladderBox;
    public GameObject ladderPlatform;
    //ladder box and top platform is declares publically to interact with player
 
    void Start()
    {
        ladderBox.SetActive(true);
        ladderPlatform.SetActive(true);
        //Sets ladders and platforms active by default
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {//If the jump button is pressed the OnEnable procedure is used
           OnEnable();
        }
       else if (Input.GetKey(KeyCode.W))
        {//If the W button is pressed the OnDisable procedure is used
         //W is the button to climb up the ladder

            OnDisable();       
        }

    }
     void OnEnable()
    {
        if (Input.GetButton("Jump"))
        {//If the jump button is pressed the ladder box is set to false
         //This is used to prevent the ladder object being detected as "ground" by player raycast
            ladderBox.SetActive(false);
        }
        

    }//end procedure
     void OnDisable()
    {
        if (Input.GetKey(KeyCode.W))
        {        
            if(!Input.GetButton("Jump"))
            {//If the W button is pressed and jump button is not pressed
                ladderBox.SetActive(true);
                //The ladder box will be set to true and therefore can be detected and climbed                
            }
            else
            {
                OnEnable();
            }            
        }       
    }//end proceudre
}//end class
