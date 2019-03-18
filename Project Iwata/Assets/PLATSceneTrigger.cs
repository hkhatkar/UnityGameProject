using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLATSceneTrigger : MonoBehaviour
{

    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {

            SceneManager.LoadScene(levelToLoad);
          
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
