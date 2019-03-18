using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject SmallbulletPrefab;
    GameObject Player;
    public PlatformPlayerMovement crouching;
    float fireRate = 0.1f;//SORT OUT NEXT TIME!!!!!!!

    private float timeBtwShots;
    public float startTimeBtwShots;
    public CameraShake cameraShake1;

    public Bullet BulletSwitch;
    GameObject bullet;

    float cameraShakeVertical = 0.2f;
    float cameraShakeHorizontal = 0.1f;

    public static bool ShieldUp = false;
    public GameObject ShieldPivotObj;

    void Start()
    {
        Player = GameObject.Find("Player");
        crouching = Player.GetComponent<PlatformPlayerMovement>();

        bullet = GameObject.Find("FirePoint");
        BulletSwitch = bullet.GetComponent<Bullet>();

    }

    // Update is called once per frame
    void Update()
    {


        if (crouching.crouch == true)//Fire1
        {
            if (Input.GetMouseButtonDown(0)) //SWITCHES BULLET SHOTS
            {
                if (Bullet.SwitchShot == true)
                {
                    Bullet.SwitchShot = false;
                    startTimeBtwShots = 1f;
                    cameraShakeHorizontal = 0.1f;
                    cameraShakeVertical = 0.2f;

                }
                else if (Bullet.SwitchShot == false)
                {

                    Bullet.SwitchShot = true;
                    startTimeBtwShots = 0.1f;
                    cameraShakeHorizontal = 0.025f;
                    cameraShakeVertical = 0.05f;

                }
                //  speedy = -10f;
            }
            //   Debug.Log("We in");
            if (timeBtwShots <= 0)
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    // Debug.Log("We in2");

                    Shoot();
                    timeBtwShots = startTimeBtwShots;
                    //  InvokeRepeating("Shoot", 5f, 5f);
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
            //  else if(Input.GetKeyDown(KeyCode.D))
            //  {
            //      InvokeRepeating("Shoot", 10f, 4);
            //  }


        }
        else if (crouching.crouch == false)
        {
            // Debug.Log("We out");
        }

        //SECTION FOR SHIELD
        if (Input.GetMouseButtonUp(1)) //SWITCHES BULLET SHOTS
        {
            
            if(ShieldUp == false)//I COULDNT GET CHECK IF SHIELDPIVOTOBJ WAS ACTIVE DIRECTLY THEREFORE I DID THIS FIRST
            {
                ShieldUp = true;
            }
            else if (ShieldUp == true)
            {
                ShieldUp = false;
            }

            if (ShieldUp == false)
            {
                ShieldRotation.Blocked = false;
                ShieldPivotObj.SetActive(false);
            }
            else if (ShieldUp == true)
            {
                
                ShieldPivotObj.SetActive(true);
            }

        }
    }

    void Shoot()//change to void
    {
        GameObject currentBullet;
        //shooting logic
        if (crouching.crouch == true)
        {
           // Debug.Log("we in 3");
            StartCoroutine(cameraShake1.Shake(cameraShakeVertical, cameraShakeHorizontal));

            if (Bullet.SwitchShot == false)
            {
                currentBullet = bulletPrefab;
            }
            else 
            {
                currentBullet = SmallbulletPrefab;
            }
            Instantiate(currentBullet, firePoint.position, transform.rotation);
        }
    }
}
