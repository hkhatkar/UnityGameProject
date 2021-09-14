using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToTrigger : MonoBehaviour
{//Level to trigger allows the user to enter a level while in TOPDOWN mode
    
    public string Level;

    void OnTriggerEnter(Collider other)
    //passes specific scene to open, when the player steps within an area which can be triggered 
    {
        PLATSceneTrigger.levelToLoad = Level;
        //Loads the level on trigger on the map

    }
}//end class
  