using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRange : MonoBehaviour
{

    public GameObject FollowScript;
    
    // Start is called before the first frame update
    void Start()
    {
        FollowScript.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D Rangecol)
    {
        if (Rangecol.CompareTag("Player"))
        {
           // Debug.Log("InCollider");
            FollowScript.SetActive(true);


            // force is how forcefully we will push the player away from the enemy.



        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FollowScript.SetActive(false);
    }
}
