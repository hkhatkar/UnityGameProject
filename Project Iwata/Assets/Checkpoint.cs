using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{//The Checkpoint class is responsible for saving a player's progress when they die.
 //If a player dies they will be sent back to the last checkpoint they triggered
    private GameMaster gm;
    //This is a private variable from GameMaster function

    void Start()
    { // Start is called before the first frame update

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //gm finds the component with the tag "GM" which will assign itself to this.
    }
    void OnTriggerEnter2D(Collider2D other)
    {//When the player collides with this trigger
        if(other.CompareTag("Player"))
        {
            gm.LastCheckPointPos = transform.position;
            //The game master will save the last checkpoint as the new position in the trigger
        }
    }//end procedure
 
}//end class
