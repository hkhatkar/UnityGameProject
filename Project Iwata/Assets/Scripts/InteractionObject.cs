using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{//this script is responsible for all objects that could be interacted with by using T (handles what happens when T is actually pressed)

    public bool inventory;
    public bool talks;  //if true then object talks to player

    public string message; //the message the object gives player
    private DialogueManager dMan;

    public bool openable; //if true object can be open
    public bool locked; //true then it is locked e.g door
    public GameObject itemNeeded; //item needed to unlock the door e.g key
    public Animator ChestAnimator;//assigned in unity therefore made public

    public string[] dialogueLines;
    public bool ChestEmpty = false;
    public GameObject ItemInChest;
    public GameObject ChestFromItemPos;
    [SerializeField] GameObject promptText;

    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>(); 
        //dialogue manager script is assigned to variable dMan
        
    }  
    public void OnTriggerStay(Collider collision)
    {
        if (promptText != null)
        {//Prompt text used only for signs. Its set active when you are in distance to read the sign
            promptText.SetActive(true);
        }

        if (collision.CompareTag("Player") && openable == true && Input.GetKeyDown(KeyCode.T))
        {//Checks if the object is openable and therefore this is responsible for handle chests
            
            if (ChestAnimator != null && ChestEmpty == false)
            {//If the chest has an animation, this will play before the chest is opened and the contents is recieved
                ChestAnimator.SetBool("Opened", true);
                ChestEmpty = true;
                StartCoroutine(ChestAnimDelay());
            }
            else
            {
                Talk();
                //Informs player that there is nothing left in the chest
            }           
        }  
    }
    public void OnTriggerExit(Collider collision)
    {
        if (promptText != null)
        {//when outside the trigger range, the sign prompt is made invisible
            promptText.SetActive(false);
        }
    }

    IEnumerator ChestAnimDelay()
    //This is used to delay giving the player the item in the chest until after the chest animation finishes
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(ItemInChest, new Vector2(ChestFromItemPos.transform.position.x, ChestFromItemPos.transform.position.y), Quaternion.identity); //HERE
    }

    public void DoInteraction()
    { 
        gameObject.SetActive(false);
    }

    public void Talk()
    {//talk interaction would use the dialogue manager
        Debug.Log(message);
        if(! dMan.dialogActive)
        {
            dMan.dialogLines = dialogueLines;
            dMan.currentLine = 0;
            dMan.ShowDialogue();
        }//Takes all the lines of dialogue in the array and assigns it to the dialogue manager to be displayed in UI form

        if (transform.GetComponent<Patrol>()  != null)
        {//if talking to a moving npc, this will stop them moving whilst they talk
            transform.GetComponent<Patrol>().canMove = false;
        }



    }
}
