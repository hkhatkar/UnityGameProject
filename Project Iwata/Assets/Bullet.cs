using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{//This class is responsible for the player's ability to shoot/ damage enemies where the player is able to switch between two weapons

    public float speedx = 0, speedy = 0, speed;
    public Rigidbody rb;
    public float lifeTime;

    public float offset;
    public GameObject ParticleBlast;

    public float thrust;
    public static bool SwitchShot;
    //SwitchShot: true means that it is quick fire, when false it is switched to heavy fire
    public static float DamageToDeal;
    public CameraShake cameraShake2;
    public Vector3 mousePos;




    void Start()
    {//Everything within start is ran before the first frame in the update procedure

        cameraShake2 = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        Invoke("DestroyProjectile", lifeTime);
        //Invoke allows for methods to be scheduled at different times causing a delay
        //Therefore after a certain time/ distance the bullet should be destroyed

        Debug.Log("bullet" + gameObject.name);
            if (gameObject.name.Contains("Small Bullet"))
            {
                DamageToDeal = 0.1f;
            }
            else
            {
                DamageToDeal = 1f;
            }
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


        if (gameObject.name.Contains("Small Bullet"))
        {           
         //   transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset + Random.Range(-10f, 10f));
        }
     
        
        
            // transform.rotation = Quaternion.Euler(0f, 0f , rotZ + offset);
             GameObject player = GameObject.Find("Player");
            
           mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
        
            //This allows the user to aim a bullet in the direction of their mouse which rotates around the player
       if (gameObject.name.Contains("FireBullet"))
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        }


    }
   void FixedUpdate()
    {
        mousePos.z = 0;
         transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePos.x, mousePos.y, 0), speed * Time.deltaTime);
       
       // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
       // Vector3 vop = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
       // transform.forward = vop;
      //  transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);

       // MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
       //// GameObject firepoint = GameObject.Find("FirePoint");
       // Transform firepointTran = firepoint.transform;

       // firepointTran.LookAt(MousePos);
        // MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //Debug.Log(MousePos +" This");
        // transform.(MousePos);
        // transform.rotation = Quaternion.FromToRotation(Vector3.up, rotZ);


        //  GameObject player = GameObject.Find("Player");
        //    Vector2 playerpos = player.transform.position;
        // Debug.Log(MousePos + "rryrhrhrrhwhrwiwhfiwfhwihfhwi");

       // transform.Translate((Vector3.forward)* 14 * Time.deltaTime);

        //Allows for the bullet to be shot from the player.
        //THe default direction is upwards and therefore this alligns the mouse with the direction of the bullet.

      //  if (speed == 0)
       // {//The bullet will be destroyed if its speed reaches 0.
          //  CameraShake.bulletDestroyed = true;
          //  if (CameraShake.bulletDestroyed == true)
          //  {
             //   Debug.Log("ouououououououuououououuouououuoouuouo");
              //  DestroyProjectile();
         //   }
            //DestroyProjectile is a separate procedure that is called when speed = 0
      //  }



   
   }


    void DestroyProjectile()
    {//Invoke is being used on this function and therefore there is a delay, bullets are shot in equal intervals.
        if (gameObject.name.Contains("PLATBullet"))
        {
            CameraShake.bulletDestroyed = true;
        }
      //  if (CameraShake.bulletDestroyed == true)
       // {
            Instantiate(ParticleBlast, transform.position, Quaternion.identity);
       // }
        Destroy(gameObject);
        
        //The object is destroyed and particles are created for effect.
    }//end procedure

   
}//end class
