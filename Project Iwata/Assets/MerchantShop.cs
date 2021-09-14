using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShop : MonoBehaviour
{//This class is responsible for displaying the shop system when the player interacts with a Non playable character
    private DialogueManager dMan;
    public GameObject Shop;

    // Start is called before the first frame update
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
        //Assigns the dialogue manager variable to an object with script "DialogueManager"
    }
    // Update is called once per frame
    void Update()
    {
        if(dMan.currentLine == 3  && Input.GetKeyDown(KeyCode.Space))
        {//The shop will be set active on the player's 4th line of dialogue when talking to the NPC
            Shop.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {//else if the space bar is pressed again the shop is disabled
            Shop.SetActive(false);
        }
    }
}//end class
