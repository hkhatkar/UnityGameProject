using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{//NON ACTIVE SCRIPT
//This class is responsible for enemy type 2 bullets to be shot towards player at a certain rate

    [SerializeField]//Serialize field allows for the variable and its value to be seen within Unity
    GameObject EnemyBullet;

    float fireRate;
    float nextFire;
    //Declares variables for rate of fire

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
        //fire rate is assigned to have a 2 second gap between each shot
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
        //The update function will constantly run this procedure in order to check the cool down has ended
    }

    void CheckIfTimeToFire()
    {//responsible for checking if next shot could be taken
        if(Time.time > nextFire)
        {//if it is over the time for next fire to recharge
            Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
            //another enemy bullet is instantiated and nextFire is reset.
        }
    }//end procedure

}//end class
