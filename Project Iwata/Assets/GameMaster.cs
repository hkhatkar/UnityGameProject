using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{//Game master class is responsible for loading the player at a check point
 //This will happen if the player dies and the scene is reloaded

    private static GameMaster instance;//instance of check point
    public Vector2 LastCheckPointPos;
    //last check point position, when player reaches a check point


    private void Awake()
    {
        if (instance == null)
        {//if there is no set instance of checkpoint
            instance = this;
            DontDestroyOnLoad(instance);
            //The instance is set to be the start area of the scene.
        }
        else
        {
            Destroy(gameObject);
            //else if the instance contains a checkpoint the object holding the script is destroyed
        }
    }
    
}//end class
