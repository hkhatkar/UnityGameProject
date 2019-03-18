using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{

    public bool inventory;
    public bool talks;  //if true then object talks to player
    public string message; //the message the object gives player
    private DialogueManager dMan;
    public bool openable; //if true object can be open
    public bool locked; //true then it is locked e.g door
    public GameObject itemNeeded; //item needed to unlock the door e.g key

    public string[] dialogueLines;
    

    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>(); //ANOTHER WAY TO REFERENCE INSTEAD OF MAKING STATIC TAKE NOTE!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    private void Update()
    {
       
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
