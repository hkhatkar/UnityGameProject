using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{//NON ACTIVE SCRIPT
//This class is responsible for NPC/enemy behaviour that will patrol a specific platform, when reaching the end it will switch sides

    public float speed;
    public float distance;
    private bool movingLeft = true;
    private DialogueManager theDM;
   
    public Transform groundDetection;

    public bool canMove;
    public bool scared;
    //Declares variables

    void Start()
    {
        scared = false;
        //Scared is a simple variable that will prevent the NPC from patrolling if an enemy comes within range
        canMove = true;
        theDM = FindObjectOfType<DialogueManager>();
        //theDM is assigned to an object containing the dialogue manager
    }

    void Update()
    {

        if(! theDM.dialogActive && scared == false)
        {//If dialogue is not active and NPC is not in enemy range
            canMove = true;
            //The NPC is able to move and patrol a platform
        }
       
        if(!canMove)
        {//if the NPC cannot move speed = 0
            speed = 0;
            return;
        }
        else
        {//else speed = 2
            speed = 2;
        }
        //This creates a raycast for the NPC called groundInfo, which will detect the edge of a platform
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        

        if (groundInfo.collider == false)
        {//If the raycast does not detect any ground...

            if (movingLeft == true)
            {//If the NPC is moving left, it will switch to right
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {//else if the NPC is moving right, it will switch to left
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
       
    }
    void OnTriggerStay2D(Collider2D NPCcol)
    {
        if (NPCcol.CompareTag("Rangecheck"))
        {//If the NPC collides with an enemies "Rangecheck"
            
            canMove = false;
            scared = true;
            //The NPC will stop patrolling
        }
       
    }//end procedure
}//end class
