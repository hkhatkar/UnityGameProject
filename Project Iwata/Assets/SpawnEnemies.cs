using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{//This class is responsible for spawning enemies
    public GameObject[] EnemyObjects;
    public int NumberSpawned;
    public Transform SpawnPoint;
    //Declares variables
    
    private bool isCoroutineExecuting = false;
    //When using update for the coroutine, it will repeat continuously therefore entering the coroutine multiple times per second
    //This causes a lot of entities to spawn and therefore could cause the program to crash
    //We want update to only execute the coroutine if the coroutine is currently not already being executed therefore this takes care of this.

    IEnumerator Spawn()
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(10f);
        //IEnumerator will wait for 10 seconds before the next enemy spawns
        
        for (int i = 0; i < NumberSpawned; i++)
        {
            GameObject EnemyClone = Instantiate(EnemyObjects[0], SpawnPoint.position, transform.rotation);
            //This will clone and enemy in a set position
        }
        isCoroutineExecuting = false;
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Spawn());
        //After each frame the coroutine will continue to iterate
    }

    
}//end class
