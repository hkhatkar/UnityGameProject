using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    public float speedx = 0, speedy = 0, speed;
    public Rigidbody2D rb;
    public float lifeTime;

    public float offset;
    public GameObject ParticleBlast;

    public float thrust;
    public static bool SwitchShot; //SWITCH SHOT = TRUE MEANS THAT IT IS MACHINE FIRE, WHEN IT IS FALSE THEN THAT IS HEAVY FIRE

    // public GameObject projectile;
    //  public Transform shotPoint;
   

    // Use this for initialization
    void Start()
    {
       
            Invoke("DestroyProjectile", lifeTime);

            DirectionInput();

            //if (speedy != 0)
         //   {
                //  speed = speedy;
         //   }
         //   if (speedx != 0)
         //   {
                //   speed = speedx;
         //   }
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        

    }
   private void Update()

    {
       



            transform.Translate(Vector2.up * speed * Time.deltaTime);



        if (speed == 0)
        {
            DestroyProjectile();
        }

       // if (speedx != 0)
       // {
      //      rb.velocity = transform.right * speed;
      //  }
     //  if(speedy != 0)
      //      {
     //       rb.velocity = transform.up * speed;
     //   }


        // speed = -20f; //changes to to left instead of right - dont disable


    }


    private void DirectionInput()
    {

        //  float speed = speed2;
       // speed = speed2;

        //use getkeydown if u want to not hold key down
        
        
        

        if (Input.GetKey(KeyCode.W))
        {
            //speedy = 10f;
           

        }


        else if (Input.GetKey(KeyCode.A))
        {
            //speedx = -10f;

            if (Input.GetKey(KeyCode.UpArrow))
            {
               // speedy += 1;
            }
        }



        else if (Input.GetKey(KeyCode.D))
        {
           // speedx = 10f;
        }


       


    }

    void DestroyProjectile()
    {
        Instantiate(ParticleBlast, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

   
}
