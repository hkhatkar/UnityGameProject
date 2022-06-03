using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{//This class is responsible for all weapon switching, fire rates and stats

    private AirShot AirShotScript;//AIR SHOT
    public Transform firePoint;
    public GameObject bulletPrefab, SmallbulletPrefab,FirebulletPrefab,BombBulletPrefab;

    GameObject Player;
    public NewPLATPlayerMovement crouching;
    float fireRate = 0.1f;
    public float timeBtwShots;//Used in AirShot
    public float startTimeBtwShots;
    //This is the time between shots after it has been passed to AirShot script to determine the speed in air
    public float tempTimeBtwShots;
    //This is the time between shots when the player is grounded and not performing an air shot
    public CameraShake cameraShake1;
    public Bullet BulletSwitch;
    GameObject bullet;
    float cameraShakeIntensity;
    public static bool ShieldUp = false;
    public GameObject ShieldPivotObj;
    public static bool ShieldSelected = false;
    public float MagicCost;
    public MagicStat MagicScript;

    void Start()
    {
        MagicScript = GameObject.FindObjectOfType<MagicStat>();
        AirShotScript = GameObject.FindObjectOfType<AirShot>();
        Player = GameObject.Find("Player");
        crouching = Player.GetComponent<NewPLATPlayerMovement>();
        //finds player object assigns it to Player

        bullet = GameObject.Find("FirePoint");
        BulletSwitch = bullet.GetComponent<Bullet>();
        //Finds bullet object and assighs to bullet

    }

    // Update is called once per frame
    void Update()
    {


        if (crouching.crouch == true)//Fire1
        {//When the player crouches they are entering aim mode allowing them to shoot
         
            if  (BulletScrollBar.selectedbulletNumber == 0)
            {//When switch shot is true the heavier weapon is activated and assigned stats below
                    Bullet.SwitchShot = false;
                    tempTimeBtwShots = 1f;

            }
            else if (BulletScrollBar.selectedbulletNumber == 1) 
            {//When switch shot is true the lighter/faster weapon is activated and assigned stats below
                    Bullet.SwitchShot = true;
                    tempTimeBtwShots = 0.1f;
                cameraShakeIntensity = 1f;
                  
            }
             else if (BulletScrollBar.selectedbulletNumber == 2) //fire bullet
            {
                Bullet.SwitchShot = false;
                tempTimeBtwShots = 0.5f;
                cameraShakeIntensity = 0.2f;
            }
            else //bomb
            {
                Bullet.SwitchShot = false;
                tempTimeBtwShots = 2f;
                cameraShakeIntensity = 0.4f;
            }
        
            startTimeBtwShots = AirShotScript.checkShootingSpeed(tempTimeBtwShots, Bullet.SwitchShot);//AIRSHOT
            if (timeBtwShots <= 0)
            {//If the time between shots is less than  or equal 0
                if (Input.GetMouseButton(0)) 
                {//If the player enters D or A whilst in aim mode they are able to call shoot procedure  
                    Shoot();
                    //Shoot procedure is called
                    timeBtwShots = startTimeBtwShots;
                    //time between shots is reset to cause a delay               
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
                //The time between shots is decremented (cool down)
            }
        }

        if (ShieldSelected == true)
        {
            //SECTION FOR SHIELD
            if (Input.GetKeyUp(KeyCode.LeftShift)) //Switches shield on
            {
                if (ShieldUp == false)
                {//IF the shield is not up, this will switch as the left click is pressed
                    AbilityManager.AbilityInUse = true;
                    ShieldUp = true;
                    ShieldPivotObj.SetActive(true);
                    //The shield pivot is set to true
                }
                else if (ShieldUp == true)
                {
                    AbilityManager.AbilityInUse = false;
                    ShieldUp = false;
                    ShieldPivotObj.SetActive(false);
                    //Else Shield pivot is deactivated and player will not be able to block
                }
            } 
        }       
    }

    void Shoot()    //SWITCHING OF BULLETS HAPPEN IN HERE 
    {//Procedure for instantiating and shooting bullet
        GameObject currentBullet;

        if (crouching.crouch == true)
        {
            if (BulletScrollBar.selectedbulletNumber == 0)
            {//If the bullet is a heavy weapon bullet current bullet is assigned to bullet prefab
                currentBullet = bulletPrefab;
                MagicCost = 10f;
                cameraShakeIntensity = 1.5f;
            }
            else if (BulletScrollBar.selectedbulletNumber == 1)
            {//else it will be assigned to the small bullet prefab
                currentBullet = SmallbulletPrefab;
                MagicCost = 1f;
                cameraShakeIntensity = 0.3f;
            }
            else if(BulletScrollBar.selectedbulletNumber == 2)
            {//else it will be assigned to the small bullet prefab
                currentBullet = FirebulletPrefab;
                MagicCost = 8f;
                cameraShakeIntensity = 1f;
            }
            else
            {
                currentBullet = BombBulletPrefab;
                MagicCost = 5f;
                cameraShakeIntensity = 0f;
            }
            Instantiate(currentBullet, firePoint.position, transform.rotation);
            CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity, 0.1f);
            MagicScript.MyCurrentValue -= MagicCost;
           
            //The bullet is instantiated from its fire point
        }
    }
}//end class
