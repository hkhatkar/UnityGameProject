using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBattleEnemy : MonoBehaviour
{//This script is responsible for spawning in the enemy that had been collided into in the 3D aspect of the game
    public static GameObject BattleEnemyToSpawn;
    public GameObject Enemy1;
 
    
    void Update()
    {
       if (BattleEnemyToSpawn != null)//going in battle
        {//BattleEnemyToSpawn is the enemy that was triggered in the 3D aspect of the game
            Debug.Log("1");
            Enemy1 = Instantiate(BattleEnemyToSpawn, new Vector2(0, 0), Quaternion.identity);
            BattleEnemyToSpawn = null;
            //This spawns the corresponding enemy in the Battleground scene
        }

        //IF all enemies in a BattleSequence die then transport back to normal 2.5D world
        if ((SceneManager.GetActiveScene().name).Contains("Battleground"))
        {
            Debug.Log("2");
            GameObject[] enemiesLeftInScene;
            enemiesLeftInScene = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemiesLeftInScene.Length == 0)
            {
                Debug.Log("3");
                //This is a key that enables to switch back to 2.5d scene in PLATSceneScript
                PLATSceneTrigger.BattleSceneComplete = true;
            }
            else
            {
                Debug.Log("" + enemiesLeftInScene);
            }
        }
    }
}
