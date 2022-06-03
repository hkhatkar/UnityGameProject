using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{//Game manager class manages the position/dimension of players when switching between game scenes
    public static GameManager Instance;
    public Vector3 spawnLocation;
    public Player p1;
    GameObject Player;
    GameObject EnemyCollided;   
    public Transform EnemyTransform;
    public bool BattleScene;//serialized version of vvv //Battle would later detect if enemy then set to true if it is
    public SpawnBattleEnemy sbe;
    public static string EnemyToBattleName = ""; //These two used in PLATSceneTrigger also
    public EnemyFollow e1;
    //declares variables

    [SerializeField]
    public GameObject[] EnemyTypes;
    //all enemies in here
    public GameObject EnemyToSpawn;
    public static List<string> TDDeadEnemies;
    public static GameObject TDEnemyCollision; 
    public static Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        TDDeadEnemies = new List<string>();
        sbe = GetComponent<SpawnBattleEnemy>();

        Player = GameObject.Find("Player");
        //Finds the game object called "Player"
        p1 = Player.GetComponent<Player>();
        //assigns p1 to an instance of player object
        //spawn location is the location assigned in unity for each room.
        //After every scene switch, the specified spawnLocation is stored within the player location
        if (Instance == null)
        {//If the Instance doesn't contain anything and therefore a scene is switched
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            //The player object will not be destroyed when switching to a new scene and will be placed in a spawn location
        }
        else
        {
            Destroy(gameObject);
            //else the gameobject holding the game manager is destroyed
        }
       
    }

    private void Update()
    {
        if (BattleScene == true && EnemyToSpawn == null)                           
        {
            
            if (EnemyToBattleName.Contains("PetraFly")) //ONLY WORKS FOR PETRAFLY ATM
            {

                EnemyToSpawn = EnemyTypes[0];
                Debug.Log("A wild " + EnemyToSpawn + "appeared");
                SpawnBattleEnemy.BattleEnemyToSpawn = EnemyToSpawn;
            }
        }
        if (PLATSceneTrigger.RestorePosition == true)
        {

            Player = GameObject.Find("Player");
            Player.transform.position = previousPosition;//make the vector3 to be previous position
            TDDeadEnemies.Add(EnemyToBattleName);
            foreach (string e in TDDeadEnemies)
            {
                Destroy(GameObject.Find(e));
            }
            //Destroy(GameObject.Find(EnemyToBattleName));//Destroy enemy in 2.5d scene that got killed
            EnemyToSpawn = null;
            PLATSceneTrigger.RestorePosition = false;
           
        }
    }

    public void SpawnEnemies()//useless?
    {
      
        Debug.Log(EnemyToSpawn.name);
        Instantiate(EnemyToSpawn, new Vector2(0, 0), Quaternion.identity);
    }



}//end class
