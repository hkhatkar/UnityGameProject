using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Pendulum : MonoBehaviour 
{//This class is responsible to create the pendulum like swing effect on the player swing ropes
    public GameObject Player;
    public PlatformPlayerMovement climb;
    float timer = 0f;
    float speed = 1f;
    int phase = 0;
    public Transform playertarget;
    public GameObject holdPosition;
    public bool Swinging;
    //Declares variables

    void Start()
    {
     
        climb = Player.GetComponent<PlatformPlayerMovement>();
        playertarget = GameObject.FindGameObjectWithTag("Swing").GetComponent<Transform>();
        //Assigns variables that will be used for the player attaching onto the rope
    }
    void FixedUpdate()
    {//Fixed update allows the frames to be cycled at equal intervals therefore keeping constant the frames per second
        if (climb.isClimbing == true && Swinging == true)
        {         
            Player.transform.position = Vector2.MoveTowards(holdPosition.transform.position, playertarget.position, speed * Time.deltaTime);
            //If the player has climbed onto the swing rope and is swinging , it will move the players position towards the outer part of the swing away from the pivot 
        }

        timer += Time.fixedDeltaTime;
        //increments the timer in order to keep the swings constant
        if (timer > 1f)
        {
            phase++;
            phase %= 4; //Keep the phase between 0 to 3.
            timer = 0f;
        }

        switch (phase)
        {//When the swing rope is swinging, there will be 4 possible cases for the direction of applied force / direction of travel
            case 0:
                transform.Rotate(0f, 0f, speed * (1 - timer));
                //Case 0 will rotate the Speed, from maximum to zero.

                if(climb.isClimbing == true && Swinging == true)
                {//if the player is swinging on the rope, the whole player including the camera will swing in a pendulum shape
                    Player.transform.Rotate(0f, 0f, speed * (1 - timer));
                    Player.transform.position = Vector2.MoveTowards(holdPosition.transform.position, playertarget.position, speed * Time.deltaTime);

                    CheckPlayerStillClimbing();
                    //Checks if the player presses space to let go of the rope                  
                }
                break;

            case 1:
                transform.Rotate(0f, 0f, -speed * timer);      
                //Case 1 will rotate Speed, from zero to maximum.
                if (climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, -speed * timer);

                    CheckPlayerStillClimbing();
                }
                break;
            case 2:
                transform.Rotate(0f, 0f, -speed * (1 - timer)); 
                //Case 2 will rotate Speed, from maximum to zero in REVERSE time (backwards).
                if (climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, -speed * (1- timer));

                    CheckPlayerStillClimbing();
                }
                break;
            case 3:
                transform.Rotate(0f, 0f, speed * timer);
                //Case 3 will rotate Speed, from zero to maximum in REVERSE time (backwards).
                if (climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, speed * timer);
                    CheckPlayerStillClimbing();
                }
                break;

        }

     
    }//end Fixed Update

    public void CheckPlayerStillClimbing()
    {//to prevent code being repeated, this procedure was created to check if the space key was pressed anytime whe swinging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            climb.isClimbing = false; 
            Player.GetComponent<Transform>().eulerAngles = new Vector2(0, 0);
            //Sets rotation of player back to 0 (standing up straight)
        }
    }//end procedure

}//end class