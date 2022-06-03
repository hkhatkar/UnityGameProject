using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHealthPowerUp : MonoBehaviour
{//This script is used when a player has a Health power up in inventory, and this button is activated with a click
//manages both its functionality and updating free space in inventory

     PlayerHealthManager PHealth;
     [SerializeField]
     GameObject healthParticles;
     GameObject playerObject;
     Inventory inventory;
     int nextFreePointer = 1;
    //nextFreePointer is used in order to inform inventory script of the next available space to fill, when collecting a new power up
   

    void Start()
    {// Start is called before the first frame update
     

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        PHealth = (GameObject.FindGameObjectWithTag("Player")).GetComponent<PlayerHealthManager>();
        //requires variables from the inventory script for managing freed up space when item is used
        //also requires health manager script in order for the power ups functionality (add 10 health when consumed)
    }
    
    public void ConsumeHealthPowerUp()
    {//This procedure is activated by a button press
        PHealth.health.MyCurrentValue += 10;
        Instantiate(healthParticles, playerObject.transform.position, Quaternion.identity);
        //increases health by 10

        Destroy(gameObject);
        inventory.isFull[nextFreePointer] = false;
        //Item is destroyed from inventory therefore can only be used once.

        if (inventory.isFull[0] == false) {nextFreePointer = 0; } 
        else if (inventory.isFull[1]== false) {nextFreePointer = 1;}
        else if (inventory.isFull[2]== false) {nextFreePointer = 2; }
        else if (inventory.isFull[3]== false) {nextFreePointer = 3;}
        else nextFreePointer = -1;
        //updates the next available space (pointer)
       

    }//end procedure
  
}//end class
