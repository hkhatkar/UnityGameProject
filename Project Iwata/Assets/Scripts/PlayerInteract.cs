using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{//This class deals with all the player interactions through pressing T
    public GameObject currentInterObj;//for npc
    public GameObject KeyObject;//for keys
    public InteractionObject KeyObjectScript;
    
    public InteractionObject currentInterObjScript;
    public Inventory inventorycall;

    GameObject temporaryStoreObject;
    InteractionObject temporaryStoreScript;
    //Declares all variables


    // Start is called before the first frame update
    void Start()
    {
        inventorycall = GetComponent<Inventory>();
        temporaryStoreObject = null;
        temporaryStoreScript = null;
        currentInterObj = null;
        currentInterObjScript = null;
        //Sets all temporarily stored objects to null
        //We would store any interactable objects in range, in these variables
        //Including keys and NPC's

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T) && currentInterObj)
        {
            //check to see if this object has a message/talks
            if (currentInterObjScript.talks)
            {
                //tell the object to give its message
                currentInterObjScript.Talk();
            }
            //check to see if this object can be opened
            if (currentInterObjScript.openable)
            {
                //check to see if the object is locked
                if (currentInterObjScript.locked)
                {
                    //check to see if we have object needed to unlock this object (key)
                    //search our inventory for the item needed if found unlock object
                    if (inventorycall.FindItem(currentInterObjScript.itemNeeded))
                    {
                        //if we found the item needed to open the door (Key)
                        currentInterObjScript.locked = false;
                        Debug.Log(currentInterObj.name + "was unlocked");
                    }
                    else
                    {
                        Debug.Log(currentInterObj.name + "not unlocked..");
                    }
                }
                else
                {
                    //object is not locked, open the object
                    Debug.Log(currentInterObj.name + "is open / unlocked");
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {//If the player is in range of an NPC...
           
            Debug.Log(other.name);
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
            //The NPC will be placed into the currentInterObj to gain the NPC's dialogue information
            if (!currentInterObjScript.talks)
            {
                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("interObject"))
        {//checks to see if an object is an interactable object (Key)      
           Debug.Log(other.name);    
           KeyObject = other.gameObject;
           KeyObjectScript = KeyObject.GetComponent<InteractionObject>();
           other.gameObject.SetActive(false);
           //If it is, it will assign KeyObject to the key game object                
        }

        if (other.CompareTag("LockedDoor"))
        {//if the player collides with the locked door and there is a key object not equal to null
            if (KeyObject != null)
            {
                if (KeyObject.CompareTag("interObject") && Input.GetKey(KeyCode.T))
                {//Player will be able to set locked door to unactive to pass through if T is pressed
                    KeyObject = null;
                    KeyObjectScript = null;
                    other.gameObject.SetActive(false);

                }
            }
        }

    }

    void OnTriggerExit(Collider other)
    {//When the player exits the NPC's trigger it will remove NPC from stored objects
        if (other.CompareTag("NPC"))
        {
            Debug.Log(temporaryStoreObject);
            currentInterObj = temporaryStoreObject;
            currentInterObjScript = temporaryStoreScript;

     
        }


    }

}//end class