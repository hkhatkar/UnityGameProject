using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingLeft = true;
    private DialogueManager theDM;
   
    public Transform groundDetection;

    public bool canMove;
    public bool scared;

    void Start()
    {
        scared = false;
        canMove = true;
        theDM = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {

        if(! theDM.dialogActive && scared == false)
        {
            canMove = true;
        }
       

        if(!canMove)
        {
            speed = 0;
            return;
        }
        else
        {
            speed = 2;
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        

        if (groundInfo.collider == false)
        {

            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
       
    }
    void OnTriggerStay2D(Collider2D NPCcol)
    {
        if (NPCcol.CompareTag("Rangecheck"))
        {
            
            canMove = false;
            scared = true;

            // force is how forcefully we will push the player away from the enemy.



        }
       // else
       // {
            //canMove = true;
       // }

    }
}
