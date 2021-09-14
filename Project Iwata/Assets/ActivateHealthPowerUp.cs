using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHealthPowerUp : MonoBehaviour
{
    PlatformPlayerMovement health;
    //Declares a variable that is recognised as an object from another class, in this class for health
   

    void Start()
    {// Start is called before the first frame update

        health = GetComponent<PlatformPlayerMovement>();
        //health is assigned to a component within the platform player movement class
        //That class is responsible for linking all the player attributes to one place
    }

    
    public void ConsumeHealthPowerUp()
    {//This procedure is activated by a button press
       
        Stat.MyMaxValue = Stat.MyMaxValue + 25;
        //25 more health is added to player's maximum health
        Stat.currentValue = Stat.MyMaxValue;
        
        //MyMaxValue is the maximum health within the class called "Stat" which holds player's Stat/Health
        //currentValue is the player's current health which is fully replenished when using this power up 
        Destroy(gameObject);
        //Item is destroyed from inventory therefore can only be used once.
       

    }//end procedure
  
}//end class
