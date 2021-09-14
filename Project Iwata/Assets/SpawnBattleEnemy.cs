using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBattleEnemy : MonoBehaviour
{
    public static GameObject BattleEnemyToSpawn;
    public GameObject Enemy1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (BattleEnemyToSpawn != null)//going in battle
        {
            Debug.Log("ok.,.....");
           Enemy1 = Instantiate(BattleEnemyToSpawn, new Vector2(0, 0), Quaternion.identity);
            
             BattleEnemyToSpawn = null;
        }




        //IF all enemies in a BattleSequence die then transport back to normal 2.5D world
        if ((SceneManager.GetActiveScene().name).Contains("Battleground"))
        {
            Debug.Log("Okay,,,,,,");
            GameObject[] enemiesLeftInScene;
            enemiesLeftInScene = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemiesLeftInScene.Length == 0)
            {
                Debug.Log("It goes in here...............................");
                //This is a key that enables to switch back to 2.5d scene in PLATSceneScript
                PLATSceneTrigger.BattleSceneComplete = true;

            }
            else
            {
                Debug.Log("" + enemiesLeftInScene);
            }

            // if (Enemy)
        }


        //if (Enemy1 == null)
    }
}
