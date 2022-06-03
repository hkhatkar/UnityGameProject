using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpeedPowerUp : MonoBehaviour
{//NON ACTIVE SCRIPT
    
    public void ConsumeSpeedPowerUp()
    {//Procedure is activated by button click in inventory
     
        PlatformPlayerMovement.runSpeed = PlatformPlayerMovement.runSpeed + 10;
        //Player's run speed from the platform player class is incremented by 10 
        Destroy(gameObject);
        //Object is destroyed so it cannot be clicked again in inventory
        
    }//end procedure

}//end class
