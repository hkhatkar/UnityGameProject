using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{//This is a simple movement class for the top down gameplay aspect

    

    [SerializeField]
    private Stat health;
    [SerializeField]
    private float initialHealth;
    //Declares two private variables that are assigned in Unity through SerializeField

    protected override void Start()
    {//Start procedure 
        transform.position = GameManager.Instance.spawnLocation; 
        health.Initialize(initialHealth, initialHealth);
        //transform.position is the position the player is located when the scene first starts up
        //This relates to another class that varies the spawn location of the player depending on where they enter from
        //health.Initialize refers to the stat class where the player's max health and current health are initialized
        base.Start();     
    }
    
	protected override void Update ()
    {
        GetInput();
        
        base.Update();      	
        //For each frame update, the GetInput() function will be ran
	}

    private void GetInput() //FOR 2D only
    {
       // direction = Vector2.zero;

        if (Input.GetKey(KeyCode.S))
        {//When the player presses S a vector movement downwards is applied 
            //direction += Vector2.down;       
        }
         if (Input.GetKey(KeyCode.W))
        {//When the player presses W a vector movement upwards is applied 
          //  direction += Vector2.up;
        }      
        if (Input.GetKey(KeyCode.A))
        {//When the player presses A a vector movement left is applied 
          //  direction += Vector2.left;
        }         
        if (Input.GetKey(KeyCode.D))
        {//When the player presses D a vector movement right is applied 
          //  direction += Vector2.right;
        }     
    }//end procedure

    


}//end class
