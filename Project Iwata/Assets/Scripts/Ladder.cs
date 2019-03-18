using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    public GameObject ladderBox;
    public GameObject ladderPlatform;
    // Use this for initialization
    void Start()
    {
      

        ladderBox.SetActive(true);
        ladderPlatform.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
           OnEnable();
        }
       else if (Input.GetKey(KeyCode.W))
        {

           OnDisable();
            
        }

        if(Input.GetKey(KeyCode.W))
        {
            
        }



    }
     void OnEnable()
    {
        if (Input.GetButton("Jump"))
        {
            Debug.Log("In1");
            ladderBox.SetActive(false);
        }
        

    }
     void OnDisable()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("In2");
            if(!Input.GetButton("Jump"))
            {
                ladderBox.SetActive(true);
                
                
            }
            else
            {
                OnEnable();
            }
            

        }

        
    }

    

}
