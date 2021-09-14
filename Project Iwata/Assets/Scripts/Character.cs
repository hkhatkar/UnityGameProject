using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{//This class is responsible for applying the movement velocity from the player class and also top down animation

    
    public static float speed;
    protected Animator myAnimator;
    protected Vector3 movement;
    private Rigidbody myRigidbody;

    private GameObject mainCam;
    private GameObject rotatetocam;
    private GameObject player;
    Vector3 gWhenStopped;
    Vector3 g;
    float x;
    float eg;
    bool walking = false;
    string  directionFacing;

    RaycastHit hit;
    Vector3 rayPosition;
    Transform rayTransform;

    GameObject DirectionsForFacing;
    Vector3 DirForFacingRotation;



    public bool IsMoving
    {
        get
        {
            return NewTDPlayerMovement.direction.x != 0 || NewTDPlayerMovement.direction.z != 0;
        }
    }
    // Use this for initialization
   protected virtual void Start ()
    {
        
        myRigidbody = GetComponentInParent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        movement = Vector3.forward;
        mainCam = GameObject.Find("Main Camera");
        rotatetocam = GameObject.Find("rotatetocam");
        player = GameObject.Find("Player");

        rayTransform = GameObject.Find("RayCentre").transform;
        rayPosition = rayTransform.position;

        DirectionsForFacing = GameObject.Find("DirectionsForFacing");
        DirForFacingRotation = DirectionsForFacing.transform.position;

        //Start procedure will assign myRigidbody to the object component of player
        //myAnimator will be assigned to the animator used for the player walking
        //The initial direction the player faces will be upwards
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (Physics.Raycast(rayPosition, Camera.main.transform.forward, out hit, 2f))
        {
            if (hit.transform.name == "DirNorth")
            {
              //  Debug.Log("North");
            }
            else if (hit.transform.name == "DirEast")
            {
               // Debug.Log("East");
            }
            else if (hit.transform.name == "DirWest")
            {
              //  Debug.Log("West");
            }
            else if (hit.transform.name == "DirSouth")
            {
             //   Debug.Log("South");
            }
            else { Debug.Log("error"); }
            
        }
            HandleLayers();

        //Procedure HandleLayers will be responsible for controlling animations based on user inputs
    }
    private void FixedUpdate()
    //fixed update is frame rate independant compared to normal update
    {
        Move();

        //The move procedure will be responsible for applying velocity at the speed selected to a player
    }
    public void Move()
    {
        // myRigidbody.velocity = movement.normalized * speed;

        //.normalized on direction is there to prevent the player moving faster when travelling diagonally      
        //This occurs when two buttons e.g. W and D are pressed at the same time to travel North East.
    }

    public void HandleLayers()
    {

        //handles animation layers     
        if (IsMoving)
        {//If the player is moving then the walk layer animation is activated through calling a procedure
            ActivateLayer("WalkLayer");

            // DirForFacingRotation = Camera.main.transform.forward;
            
           // DirectionsForFacing.transform.LookAt( mainCam.transform);
            
            //set walk animation to on
            if (Input.GetKey(KeyCode.W))
            {
                myAnimator.SetFloat("x", 0);
                myAnimator.SetFloat("y", 1);
                directionFacing = "Forward";
                DirectionsForFacing.transform.rotation = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0);

            }
            else if (Input.GetKey(KeyCode.S))
            {
                myAnimator.SetFloat("x", 0);
                myAnimator.SetFloat("y", -1);
                directionFacing = "Backward";
                DirectionsForFacing.transform.rotation = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y + 180, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                myAnimator.SetFloat("x", 1);
                myAnimator.SetFloat("y", 0);
                directionFacing = "Right";
                DirectionsForFacing.transform.rotation = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y + 90, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                myAnimator.SetFloat("x", -1);
                myAnimator.SetFloat("y", 0);
                directionFacing = "Left";
                DirectionsForFacing.transform.rotation = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y +270, 0);
            }

            walking = true;

        }
        else
        {//else the player is not moving and the idle animations are played
            if (walking == true)
            {
                // if (directionFacing.Equals("Forward"))
                // {
                //     string currentForwardBox = hit.transform.name;
                // }

                DirForFacingRotation = DirectionsForFacing.transform.position;
                //x = DirectionsForFacing.transform.eulerAngles.y;

                
                walking = false;
                

            }
            ActivateLayer("IdleLayer");
            if (Physics.Raycast(rayPosition, Camera.main.transform.forward, out hit, 2f))
            {
                if (hit.transform.name == "DirNorth")
                {
                    Debug.Log("North");
                    myAnimator.SetFloat("y", 1);
                    myAnimator.SetFloat("x", 0);
                  
                  
                }
                else if (hit.transform.name == "DirEast")
                {
                    Debug.Log("East");
                    myAnimator.SetFloat("y", 0);
                    myAnimator.SetFloat("x", -1);
                }
                else if (hit.transform.name == "DirWest")
                {
                    Debug.Log("West");
                    myAnimator.SetFloat("y", 0);
                    myAnimator.SetFloat("x", 1);
                }
                else if (hit.transform.name == "DirSouth")
                {
                    Debug.Log("South");

                    myAnimator.SetFloat("y", -1);
                    myAnimator.SetFloat("x", 0);
                }
                else { Debug.Log("error"); }

            }


            /*      g = mainCam.transform.eulerAngles; 
                  eg = g.y;

                  ActivateLayer("IdleLayer");



                   if (eg > 360)
                  {
                     eg = eg - 360;
                   }
                  if (eg < 0)
                  {
                      eg = eg + 360;
                  }
                  //Debug.Log(eg + " g " + g + " x " + x);


                  float frontface;
                  if (x >= 180)
                  {
                      frontface = x - 180;
                  }
                  else
                  {
                      frontface = x + 180;
                  }


                  if (eg > (frontface - 45 - directionShift) && eg < (frontface + 45 - directionShift) ||                              
                         (((frontface - 45 - directionShift) < 0) && (eg > (360 + (frontface - 45 - directionShift))) && eg < 360) ||   //if facing right range  for frontface OR the end or max range for frontface go outside 360 and 0...
                         (((frontface + 45 - directionShift) > 360) && (eg < (frontface + 45 - directionShift) - 360)) && eg > 0    )    //calculate new range by minusing 360 or adding 360 to max / min respectively and check between...
                                                                                                                                          // 0 to max, and min to 360, if in that range then enter if !                                                                                                                      
                  {               
                      myAnimator.SetFloat("y", -1);
                      myAnimator.SetFloat("x", 0);
                  //facing camera     
              }

               else if (eg > (frontface - 135 - directionShift) && eg < (frontface - 45 - directionShift) ||                             
                           (((frontface - 135 - directionShift) < 0) && (eg > (360 + (frontface - 135 - directionShift))) && eg < 360) ||   
                           (((frontface -45 - directionShift) > 360) && (eg < (frontface -45 - directionShift) - 360)) && eg > 0)
                  {
                  myAnimator.SetFloat("x", -1);    
                  myAnimator.SetFloat("y", 0);



              }//facing left

                  else if (eg > (frontface - 225 - directionShift) && eg < (frontface - 135 - directionShift) ||
                             (((frontface - 225 - directionShift) < 0) && (eg > (360 + (frontface - 225 - directionShift))) && eg < 360) ||
                             (((frontface - 135 - directionShift) > 360) && (eg < (frontface - 135 - directionShift) - 360)) && eg > 0)
                  { 
                      myAnimator.SetFloat("y", 1);                                
                      myAnimator.SetFloat("x", 0);
                  }//looking forward
                  else
                  {

                      myAnimator.SetFloat("x", 1);
                      myAnimator.SetFloat("y", 0);
                  }//facing right
                  */

        }//else set the walk animation weight to 0 therefore it does not play walk animation  


    }//end procedure


    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount;i++)
        {
            myAnimator.SetLayerWeight(i, 0);
            //Each frame/ sprite of the player is played out using a for loop
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
        //Weight is changed in order to switch between idle and walk layer

    }//end procedure
}//end class
