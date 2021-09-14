using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{//This class is responsible for controlling the wall jump feature for the player's movement
 //THIS IS DEPENDANT ON LADDER RAYCAST = change speed here = change speed up ladder

    Vector3 currentWallMovement;//////////////////////////////////////////////////////////
    public static float wallJumpPower = 17f;//   HERE
    public float currentWallJumpPower;//////////////////////////////////////////////////////////

    public CharacterController chara;
    CharacterController2D movement;
    PlatformPlayerMovement PlayerSpeed;
    public float Wallspeed = 10f;
    public float distance;
    Vector3 directionOfRay = Vector3.right;
    public LayerMask WhatIsWall;
    public GameObject WallParticles;
    public int dirFace;
    public bool changeDirectionFacing;
    NewPLATPlayerMovement PlayerMoveScript;
    //Declares variables
   
    // Start is called before the first frame update
    void Start()
    {
        changeDirectionFacing = false;
        PlayerMoveScript = gameObject.GetComponent<NewPLATPlayerMovement>();
       // movement = GetComponent<CharacterController2D>();
      //  PlayerSpeed = GetComponent<PlatformPlayerMovement>();

        //assigns movement to the component attached to CharacterController2D script
        //PlayerSpeed is assigned to the speed component in PlatformPlayerMovement
    }

    // Update is called once per frame
    void Update()
    {

       // currentWallMovement = new Vector3(currentWallJumpPower, 0, 0);
       // chara.Move(currentWallMovement * Time.deltaTime);

        if (PlayerMoveScript.anim.GetBool("PLATx") == true &&  Input.GetKey(KeyCode.A))
        {
            directionOfRay = Vector3.left;
            dirFace = -1;
            changeDirectionFacing = false;
        }
        else if (PlayerMoveScript.anim.GetBool("PLATx") == false && Input.GetKey(KeyCode.D))
         //   (Input.GetKeyDown(KeyCode.D) || (changeDirectionFacing == true && dirFace == -1))
        {
            directionOfRay = Vector3.right;
            dirFace = 1;
            changeDirectionFacing = false;
        }
        //determines direction of the way you want to walljump from

        RaycastHit hit;
        Debug.DrawRay(new Vector3(0f, 1f, 0) + transform.position, directionOfRay,  Color.cyan, distance);
        //Creates a raycast that is used to detect a wall
        if (Physics.Raycast(new Vector3(0f, 1f, 0f)  + transform.position, directionOfRay , out hit,distance*2))
        {
            Debug.Log("waw:" + hit.collider.name);
            if (Input.GetKey(KeyCode.Space) && hit.collider.tag == "Wall") // !movmenent.Grounded !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                // currentWallJumpPower = wallJumpPower;
                if (dirFace == -1)
                {
                    
                    PlayerMoveScript.HandleWallJump( wallJumpPower);
                   
                }
                else
                {
                   
                    PlayerMoveScript.HandleWallJump(-1*  wallJumpPower);
                   
                }
              //  Debug.Log("Indoe");
               // chara.enabled = false;
               // GetComponent<CapsuleCollider>().enabled = true;
              //  GetComponent<Rigidbody>().velocity = new Vector3(Wallspeed * (hit.normal.x * 2), Wallspeed); //CHANGE TO AFFECT CHAR CONTROLLER NOT RIGID BODY
             //   //hit.normal.x is multiplied to give a horizontal thrust
              //  Debug.Log("RigidVel =" + GetComponent<Rigidbody2D>().velocity);
              //  Instantiate(WallParticles, transform.position, Quaternion.identity);
                
                
                //Velocity is appled to the opposite direction when the player presses space next to wall and wall particles are displayed          
            }
        }
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
       // Gizmos.DrawLine(transform.position, directionOfRay* transform.localScale.x);
       // Gizmos.DrawLine(new Vector3(0f * transform.localScale.x, 1f, 0) +transform.position, new Vector3(1f * dirFace, 1f, 0) + transform.position + directionOfRay * dirFace *(distance));
     //   Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
        //This is used in order to visually see the ray within run time and therefore helps for debugging
    }
}//end class
