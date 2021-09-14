using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{//This class is responsible for allowing the player to grab, certain objects and throw them applying a force

    public bool grabbed;
    RaycastHit hit;
 
    public float distance = 2f;
    public Transform holdPoint;
    GameObject LiftedObject;
    Collider ObjectCollider;
    Ray reachableray;
    bool FacingTowards = false;
    public float throwForce;
    //Declares all variable   

    // Update is called once per frame
    void Update()
    {
        if (PlatformPlayerMovement.FacingLeft == false)
        {
            reachableray = new Ray(transform.position, Vector3.right);
        }
        else
        {
            reachableray = new Ray(transform.position, Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {//B is the key that will be used to pick up an item
            if(!grabbed)
            {//If the B button is pressed however player hasn't picked up an object

                ///Physics2D.queriesStartInColliders = false;

                //A raycast is created to check if an item is in range
                //The raycast is starting at a point which is already inside an object (the player)
                //This prevents the player being detected as a liftable object
                // if (PlatformPlayerMovement.FacingLeft == false)    
                // {//If the player is facing right...
                if (Physics.Raycast(reachableray, out hit, 5.0f) && hit.collider.tag == ("Grabbable")) //&& PlatformPlayerMovement.FacingLeft == false)
                {
                    FacingTowards = true;
                    Debug.Log("right");
                    AbilityManager.AbilityInUse = true;
                    grabbed = true;
                    LiftedObject = hit.collider.gameObject;
                    ObjectCollider = LiftedObject.GetComponent<Collider>();
                    ObjectCollider.enabled = false;
                    //The object is picked up and placed above the player's head
                }
                //else if (Physics.Raycast(reachableray, out hit, -5.0f) && PlatformPlayerMovement.FacingLeft == true)
                // //{
                //    FacingTowards = true;
                //    Debug.Log("left");
                //  }  
                else
                {
                    FacingTowards = false;
                }
                //The raycast is projected in the right direction of the playerif (hit.collider != null && hit.collider.tag == "Grabbable" && FacingTowards == true)
                //if the raycast "hit" detects an object with a tag containing "Grabbable"
                   
                
            }
            //  else
            //  {//else the raycast is projected in the left direction of the player
            //      hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x);
            //  }
            //raycast checks if object is in range

                       
            //}
            
            else
            //else if we are holding something and b is pressed again
            {
                if ((hit.collider != null && hit.collider.tag == "Grabbable"))
                {                   
                        grabbed = false;
                        ObjectCollider.enabled = true;
                        if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                        {//throw force applied when player decides to let go of object by pressing b again

                            if (PlatformPlayerMovement.FacingLeft == true)//left side throw
                            {
                            Debug.Log("Facing Left");
                            hit.collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3((-transform.localScale.x), 1.5f,0) * throwForce;
                            //A force is applied to the left side of the object being picked up
                            }
                            if (PlatformPlayerMovement.FacingLeft == false)//right side throw
                            { 
                            Debug.Log("Facing Right");
                            hit.collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(transform.localScale.x, 1.5f,0) * throwForce;
                            //A force is applied to the right side of the object being picked up
                            AbilityManager.AbilityInUse = false;
                        }
                    }                                 
                }
            }         
        }
        if (Input.GetKeyDown(KeyCode.W) && grabbed == true)// if climbing up a ladder.. (was a bug but now fixed)
        //even when not on ladder, it will drop the item it is holding
        {
            grabbed = false;
            Physics2D.queriesStartInColliders = true;
            //As the raycast starts inside a collider (the player) it will ignore this object being detected
            if(grabbed == false)
            {
                if (ObjectCollider != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-transform.localScale.x, 1,0) * throwForce;//>>                  
                    ObjectCollider.enabled = true;
                    AbilityManager.AbilityInUse = false;
                    //Drops the item that has been grabbed if the player decides to climb up a ladder
                }
            }
        }
       

        if (grabbed)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;  
            //Places the object being picked up above the players position
        }
    }

    void OnDrawGizmos()
    {//This procedure is used as a debugging tool in order to allow programmers to physically see raycast drawn on the screen
        Gizmos.color = Color.blue;//Colour of this raycast is set to blue
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);        
    }//end procedure
}//end class
