using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneCave1 : MonoBehaviour {

    public GameObject guiObject;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
        guiObject.SetActive(false);
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		
        if (other.gameObject.tag == "Player")
        {
            guiObject.SetActive(true);
            if(guiObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
            {
                Application.LoadLevel(levelToLoad);
            }
        }
	}

    void OnTriggerExit()
        {
        guiObject.SetActive(false);
        }
}
