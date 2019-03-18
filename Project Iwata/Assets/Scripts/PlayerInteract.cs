using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterObj;//for npc
    public GameObject KeyObject;//for keys
    public InteractionObject KeyObjectScript;
    
    public InteractionObject currentInterObjScript;
    public Inventory inventorycall;

    GameObject temporaryStoreObject;
    InteractionObject temporaryStoreScript;


    // Start is called before the first frame update
    void Start()
    {
        inventorycall = GetComponent<Inventory>();
        temporaryStoreObject = null;
        temporaryStoreScript = null;
        currentInterObj = null;
        currentInterObjScript = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && currentInterObj)
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
                        //we found item needed
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
           
            Debug.Log(other.name);

            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
            if (!currentInterObjScript.talks)
            {
                other.gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("interObject"))
        {
         
            Debug.Log(other.name);

            //currentInterObj = other.gameObject;
            KeyObject = other.gameObject;
           KeyObjectScript = KeyObject.GetComponent<InteractionObject>();
        
                other.gameObject.SetActive(false);
      
        }

        if (other.CompareTag("LockedDoor"))
        {
            if (KeyObject != null)
            {


                if (KeyObject.CompareTag("interObject") && Input.GetKeyDown(KeyCode.T))
                {
                    KeyObject = null;//was currentinterobj
                    KeyObjectScript = null;
                    other.gameObject.SetActive(false);

                }
            }
        }





    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            Debug.Log(temporaryStoreObject);
            currentInterObj = temporaryStoreObject;
            currentInterObjScript = temporaryStoreScript;

            //currentInterObj = null;
            //currentInterObjScript = null;
            // other.gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("interObject"))
        {
            //  if (currentInterObj != null)
            // {
         //   temporaryStoreObject = currentInterObj;
            //temporaryStoreScript = currentInterObjScript;
            // }
        }
    }
}