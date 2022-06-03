using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cave1Trigger : MonoBehaviour
{//NON ACTIVE SCRIPT
//This class is responsible for switching scenes between top down 2D to the first 2D platform style cave/dungeon

    public Vector2 SpotInNextRoom;
    GameObject persistancescene;
    public GameManager sp;
    public string levelToLoad;
    public bool levelLoaded;
    //Variables are declared which are assigned within unity

    void Start()
    {//Before the scene starts...
        persistancescene = GameObject.Find("persistancescene");
        sp = persistancescene.GetComponent<GameManager>();
        //When entering the new scene, the player shouldn't start anywhere in the next level.
        //The player needs to start from the cave entrance. Persistance scene is an object holding the Vector location...
        //... of the next scene and therefore whenever landing on this specific trigger it will remember the location for the player
    }
    void OnTriggerEnter2D(Collider2D other)
    {//When the player collides within the entrance to next cave (box collider with trigger)
      
        if (other.gameObject.name == "Player")
        {//if the object that collided into the scene trigger is the player...
            
            SceneManager.LoadScene(levelToLoad);
            sp.spawnLocation = SpotInNextRoom; 
            //The scene manager will load the "levelToLoad" assigned in unity
            //It will also assign its location in the next room
        }
      
    }
    public void OnDoorEntered()
    {
        GameManager.Instance.spawnLocation = SpotInNextRoom; 
        //the location in the next room is loaded
    }
}
