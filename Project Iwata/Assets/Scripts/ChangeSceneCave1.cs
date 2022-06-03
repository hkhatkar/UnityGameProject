using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneCave1 : MonoBehaviour {
//NON ACTIVE script
//Script used to load a new scene when the pllayer enters into a trigger

    public GameObject guiObject;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
        guiObject.SetActive(false);
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
	//if something has entered the triggered area, this method would be called taking the object that entered as a parameter

        if (other.gameObject.tag == "Player")
        {//checks entered object is player
            guiObject.SetActive(true);
            if(guiObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
            {               
                SceneManager.LoadScene(levelToLoad);
            }
        }
	}

    void OnTriggerExit()
        {
            guiObject.SetActive(false);
        }
}
