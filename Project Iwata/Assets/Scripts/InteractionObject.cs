using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{//this script is responsible for 

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

    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>(); //ANOTHER WAY TO REFERENCE INSTEAD OF MAKING STATIC TAKE NOTE!!!!!!!!!!!!!!!!!!!!!!!!!!
        
    }

    private void Update()
    {
       
    }
    
    public void OnTriggerStay(Collider collision)
    {
        
        if (collision.CompareTag("Player") && openable == true && Input.GetKeyDown(KeyCode.T))
        {//All for chest
            
            if (ChestAnimator != null && ChestEmpty == false)
            {
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
    IEnumerator ChestAnimDelay()
    //This is used to delay giving the player the item in the chest until after the chest animation finishes
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(ItemInChest, new Vector2(ChestFromItemPos.transform.position.x, ChestFromItemPos.transform.position.y), Quaternion.identity); //HERE
    }

    // Update is called once per frame
    public void DoInteraction()
    {
        gameObject.SetActive(false);
    }

    public void Talk()
    {
        Debug.Log(message);
        //dMan.ShowBox(message);
        if(! dMan.dialogActive)
        {
            dMan.dialogLines = dialogueLines;
            dMan.currentLine = 0;
            dMan.ShowDialogue();
        }
        if (transform.GetComponent<Patrol>()  != null)//NOT RIGHT
        {
            transform.GetComponent<Patrol>().canMove = false;
        }



    }
}
