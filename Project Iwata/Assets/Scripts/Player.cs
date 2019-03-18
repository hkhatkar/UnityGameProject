using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat health;

    
    [SerializeField]
    private float initialHealth;

 



    protected override void Start()
    {
        transform.position = GameManager.Instance.spawnLocation;
        health.Initialize(initialHealth, initialHealth);
        base.Start();
        
    }
    

    

    
  //ami,ator wass here

  //direction vector 2 was here

	// Use this for initialization
	//void Start () {

       //animatoer
        //direction = Vector2.up;
	//}
	
	// Update is called once per frame
	protected override void Update () {

     
        GetInput();

        base.Update();
       

        

        
		
	}

    //move was here
    
        //animate movement was here

    private void GetInput()
    {
       
        direction = Vector2.zero;

        //This is for debugging only!!////////

        if (Input.GetKeyDown(KeyCode.I)) //damage
        {
            health.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O)) //heal
        {
            health.MyCurrentValue += 10;
        }


        //////////////////////////////////////


        //use getkeydown if u want to not hold key down

        if (Input.GetKey(KeyCode.S))
        {
            
            direction += Vector2.down;
            
        }

         if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
          

        }
       
       
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
         
        

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }


       
    }
}
