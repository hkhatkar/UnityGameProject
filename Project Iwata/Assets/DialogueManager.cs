using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

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
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogActive && Input.GetKeyUp(KeyCode.Space))
        {
            // dBox.SetActive(false);
            //dialogActive = false;

            currentLine++;

            
        }

        if(currentLine >= dialogLines.Length)
        {
            dBox.SetActive(false);
            dialogActive = false;

            currentLine = 0;
            thePlayer.canMove = true;        //---This needs to be fixed, this is meant to stop player in platform mode from moving but it doesnt work for top down mode
        }
        dText.text = dialogLines[currentLine];

    }

    public void ShowBox(string dialogue)
    {
        dialogActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }

    public void ShowDialogue()
    {
        dialogActive = true;
        dBox.SetActive(true);

        thePlayer.canMove = false;          // ---This needs to be fixed, this is meant to stop player in platform mode from moving but it doesnt work for top down mode
    }
}
