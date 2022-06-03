using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;//TMPro allows for better graphical display of text

public class DialogueManager : MonoBehaviour
{//The dialogue manager, manages NPC's interactions when "T" is pressed. Dialogue will be assigned within Unity

    public GameObject dBox;
    public TextMeshProUGUI dText;

    public bool dialogActive; 

    public string[] dialogLines;
    public int currentLine;

    private PlatformPlayerMovement thePlayer;
    private Player thePlayer2;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlatformPlayerMovement>();   
        //This assigns thePlayer to the object containing PlatformPlayerMovement (Player)
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.E))
        {//If there is still dialogue active within the NPC's array

            currentLine++;
            //If the player presses Space the next line of dialogue will appear
        }

        if (currentLine >= dialogLines.Length)
        {//If the currentLine is more that or equal to the dialogue Line array...
            dBox.SetActive(false);
            dialogActive = false;
            currentLine = 0;
            thePlayer.canMove = true;
            //The dialogue box will be disabled as end of text has been reached.
            //The currentline is reset and the player iwll be able to move again
        }
        if (dialogLines.Length != 0)
        {
            dText.text = dialogLines[currentLine];
        }
    
        //This displays the current line of dialogue
    }

    public void ShowBox(string dialogue)
    {//Procedure activated when player is within NPC's range and presses T
        dialogActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
        //Dialogue boxes and text will be set to true and first line of dialogue is read
    }//end procedure

    public void ShowDialogue()
    {
        dialogActive = true;
        dBox.SetActive(true);
        thePlayer.canMove = false;      
        //Player is frozen into position and dialogue is displayed
    }//end procedure
}//end class
