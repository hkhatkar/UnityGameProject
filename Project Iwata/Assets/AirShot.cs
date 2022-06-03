using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShot : MonoBehaviour
{//NON ACTIVE SCRIPT

    public GameObject Player;//assigned in unity
    public PlatformPlayerMovement Climb;
    private bool globalgrounded ;
    private bool globalcrouching ;
    private float globalTimeOfShots ;
    public GameObject SlowMotionEffect1;//assigned in unity
    public GameObject SlowMotionEffect2;//assigned in unity
    public GameObject SlowMotionController;//assigned in unity
    public AudioSource BackgroundMusic;
    float InitialPitch;
    public bool SlowMoActivated = false;



    void Start()
    {
        // BackgroundMusic = GetComponent<AudioSource>();
        InitialPitch = BackgroundMusic.pitch;
        Climb = Player.GetComponent<PlatformPlayerMovement>();
        Time.timeScale = 1f;
    }

    public void checkIfInAir(bool grounded)//Checks by passing Grounded from Character controller class
    {
        globalgrounded = grounded;
       // Debug.Log("Player grounded " + grounded);

    }
    public void checkIfCrouching(bool crouch)//Checks by passing crouch from Platform Player Movement class
    {
     
        globalcrouching = crouch;

    }
    public float checkShootingSpeed(float TimeOfShots, bool SwitchShot)//Checks by passing crouch from Platform Player Movement class
    {
        float TimeOfShotChanged;
        float SlowMoMultiplier = 1f;
    
        globalTimeOfShots = TimeOfShots;                               

 
        
            if (BulletScrollBar.selectedbulletNumber == 0) //SWITCHES THE BULLETS BACK TO ORIGINAL SHOOT SPEED, WHEN MAKING NEW BULLETS CHANGE THESE VALUES TOO !!!!
            {
                TimeOfShotChanged = 0.75f * SlowMoMultiplier;
            }
            else if (BulletScrollBar.selectedbulletNumber == 1)
            {
                TimeOfShotChanged = 0.1f * SlowMoMultiplier;
            }
            else if (BulletScrollBar.selectedbulletNumber == 2) 
            {
                TimeOfShotChanged = 0.5f * SlowMoMultiplier;
            }
            else
            {
                TimeOfShotChanged = 5f * SlowMoMultiplier;
            }
        

        Debug.Log(TimeOfShotChanged);
        return TimeOfShotChanged;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SlowMotionEffect1.transform.Rotate(2, 2, 2);
        SlowMotionEffect2.transform.Rotate(-2, -2, -2);


        if (globalgrounded == false && Input.GetMouseButton(0)) 
        {
            
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime =  Time.timeScale * 0.02f;
            Debug.Log("Slow Motion Activated!");
            //Weapon.timeBtwShots = 0;//Timer resets
            SlowMotionController.SetActive(true);
            if (SlowMoActivated == false)
            {
                SlowMoActivated = true;
            }

        }
        else if (globalgrounded == true) 
        {
         
           Time.timeScale = 1f;
           Time.fixedDeltaTime =  Time.timeScale * 0.02f;
           SlowMotionController.SetActive(false);
            SlowMoActivated = false;
        }
    
    }
}
