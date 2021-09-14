using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRange : MonoBehaviour
{//This script is used to limit the range in which an enemy could follow you in
 //If the enemy is outside this range, it will not follow you.

    public GameObject FollowScript;
    
    // Start is called before the first frame update
    void Start()
    {
        FollowScript.SetActive(false);
        //disables enemy follow script by default
    }


    void OnTriggerStay(Collider Rangecol)
    {
        
        if (Rangecol.CompareTag("Player"))
        {//If the player is in the enemy trigger zone (assigned in unity box collider)...
         
            FollowScript.SetActive(true);
            //The follow script will be set to true (EnemyFollow Script) and enemy will follow player
            
        }     
    }
    private void OnTriggerExit(Collider collision)
    {//When the player exits the enemy's trigger zone the script will deactivate and the player will not be followed
        FollowScript.SetActive(false);
    }//end procedure
}//end class
