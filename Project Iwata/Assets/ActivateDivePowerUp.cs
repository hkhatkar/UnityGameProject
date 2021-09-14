using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDivePowerUp : MonoBehaviour
{
    PlatformPlayerMovement PlayerMass;
    Rigidbody PlayerRigidBody;
    bool ClickedOnce = false;//This power up lasts until the player jumps, once for adding weight to player so they can dive and once to emerge at surface again
    //Declares a variable that is recognised in this class for Player's mass and rigid body
    //This is assigned within Unity engine
 
    public void ConsumeDivePowerUp()
    {//This procedure is triggered by a button click

        if (ClickedOnce == false)
        {
            PlayerMass = GetComponent<PlatformPlayerMovement>();
            PlayerRigidBody = PlatformPlayerMovement.rb;
           PlayerRigidBody.mass = PlayerRigidBody.mass * 30;
            ClickedOnce = true;
            //When the button to dive is clicked within players inventory, the player gains a mass that is 3 times its original weight
            //This will overcome the upthrust force from water
        }
        else if (ClickedOnce == true && !Input.GetButton("Jump"))
        {//When player next jumps it will remove the weight
           PlayerRigidBody.mass = PlayerRigidBody.mass / 30;
            PlayerRigidBody.AddForce(new Vector2(0f, 1000f));
            Destroy(gameObject);
            //Destroys the diving power up in users inventory therefore can only be clicked once

        }
        

       
    }//end procedure

}//end class
