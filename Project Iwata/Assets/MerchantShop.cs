using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShop : MonoBehaviour
{
    private DialogueManager dMan;
    public GameObject Shop;
    // Start is called before the first frame update
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dMan.currentLine == 3  && Input.GetKeyDown(KeyCode.Space))
        {
            Shop.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            Shop.SetActive(false);
        }
    }
}
