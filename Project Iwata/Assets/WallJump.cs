using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour //THIS IS DEPENDANT ON LADDER RAYCAST = change speed here = change speed up ladder
{

    CharacterController2D movement;
    PlatformPlayerMovement PlayerSpeed;
    public float Wallspeed = 10f;
    public float distance;
    Vector2 directionOfRay;
    public LayerMask WhatIsWall;
    public GameObject WallParticles;
   
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<CharacterController2D>();
        PlayerSpeed = GetComponent<PlatformPlayerMovement>();
       // Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            directionOfRay = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            directionOfRay = Vector2.right;
        }
        //determines direction of the way you want to walljump from

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionOfRay * transform.localScale.x, distance, WhatIsWall);// = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
                                                                                                                    //creates the ray
     //   Debug.Log(hit.collider + "------------------------------");
        if (Input.GetKeyDown(KeyCode.Space) && !movement.m_Grounded && hit.collider  != null)

        //&& !movement.jump 
        {
          
            
                GetComponent<Rigidbody2D>().velocity = new Vector2(Wallspeed * (hit.normal.x * 2), Wallspeed); //hit.normal.x is multiplied to give a horizontal thrust
            Debug.Log("RigidVel ====" + GetComponent<Rigidbody2D>().velocity);
             Instantiate(WallParticles, transform.position, Quaternion.identity);
            //   PlayerSpeed.speed = Wallspeed * hit.normal.x; --THIS IS WHATLADDER WAS DEPENDANT ON BUT DISABLED IT FOR NOW (DONT DELETE)




            // transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;   --MAKES EVERYTHING DISAPEAR DONT USE
            // }
        }
    
       
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * transform.localScale.x * distance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
        //Draws a ray but not assigns it / creates it
    }
}
