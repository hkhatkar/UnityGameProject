using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
//NON ACTIVE SCRIPT
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
   
            HandleLayers();

        //Procedure HandleLayers will be responsible for controlling animations based on user inputs
    }
   

    public void HandleLayers()
    {

        //handles animation layers     
        if (IsMoving)
        {//If the player is moving then the walk layer animation is activated through calling a procedure
            ActivateLayer("WalkLayer");
            
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
                DirForFacingRotation = DirectionsForFacing.transform.position;         
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


 
        }
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
