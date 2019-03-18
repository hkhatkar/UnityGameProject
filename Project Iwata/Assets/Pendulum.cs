using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Pendulum : MonoBehaviour //need to change later!

{
    public GameObject Player;
    public PlatformPlayerMovement climb;
    float timer = 0f;
    float speed = 1f;
    int phase = 0;
    public Transform playertarget;
    public GameObject holdPosition;
    public bool Swinging;
    void Start()
    {
        climb = Player.GetComponent<PlatformPlayerMovement>();
        playertarget = GameObject.FindGameObjectWithTag("Swing").GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (climb.isClimbing == true && Swinging == true)
        {
            
            Player.transform.position = Vector2.MoveTowards(holdPosition.transform.position, playertarget.position, speed * Time.deltaTime);
            
        }

        timer += Time.fixedDeltaTime;
        if (timer > 1f)
        {
            phase++;
            phase %= 4;            //Keep the phase between 0 to 3.
            timer = 0f;
        }

        switch (phase)
        {
            case 0:
                transform.Rotate(0f, 0f, speed * (1 - timer));  //Speed, from maximum to zero.

                if(climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, speed * (1 - timer));
                    Player.transform.position = Vector2.MoveTowards(holdPosition.transform.position, playertarget.position, speed * Time.deltaTime);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        climb.isClimbing = false; //PUT INTO SEPARATE FUNCTION---------------------------------------------------------------------------------
                        Player.GetComponent<Transform>().eulerAngles = new Vector2(0, 0);
                    }
                }

                break;
            case 1:
                transform.Rotate(0f, 0f, -speed * timer);       //Speed, from zero to maximum.
                if (climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, -speed * timer);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        climb.isClimbing = false;
                        Player.GetComponent<Transform>().eulerAngles = new Vector2(0, 0);
                    }
                }
                break;
            case 2:
                transform.Rotate(0f, 0f, -speed * (1 - timer)); //Speed, from maximum to zero.
                if (climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, -speed * (1- timer));
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        climb.isClimbing = false;
                        Player.GetComponent<Transform>().eulerAngles = new Vector2(0, 0);
                    }
                }
                break;
            case 3:
                transform.Rotate(0f, 0f, speed * timer);        //Speed, from zero to maximum.
                if (climb.isClimbing == true && Swinging == true)
                {
                    Player.transform.Rotate(0f, 0f, speed * timer);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        climb.isClimbing = false;
                        //climb.isClimbing = false;
                        Player.GetComponent<Transform>().eulerAngles = new Vector2(0, 0);
                    }
                }
                break;

        }

        
            //Player.transform.Rotate(0, 0 , 10);
        

    }
   // void OnTriggerStay2D(Collider2D collision)
 //   {
    //        if(.CompareTag("Swing"))
   //         {
    //       
   //         Swinging = true;
    //        }
          // else
          // {
         //   Swinging = false;
          // }
   // }

}