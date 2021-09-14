using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PLATSceneTrigger : MonoBehaviour
{
    public Animator Fade_animator;
    [SerializeField]//this prevents the programmer changing the data levelToLoad holds, as we want this only to change when a player sets a trigger by entering a door
    public static string levelToLoad; // this is set as public static, so it can be accessed by any class, this is needed because the scene that needs to be loaded changes depending on which event/ door has been triggered
    public string MenuLevelStarter; //same for levelToLoad but works only with main menu button

    //Shader transition vvv
    public Image blackScreen;
    public float transitionSpeed = 1f;
    //private float transitionProgress = 0f;
    //private bool transitionStarted = false;
    public static bool shouldReveal;

    GameManager gameManagerScript;
    GameObject persistantSceneObject; // For GameManager script

    Scene scene;
    GameObject getPersistanceScene;

    public static bool BattleSceneComplete = false; // need to be static, only can be 1
    public GameObject Player;

    public static bool RestorePosition = false;
   // public static GameObject TDEnemy

    public GameObject VictoryText;

    public void Start()
    {

   

        Player = GameObject.Find("Player");
        //Player.transform.position = GameManager.Instance.spawnLocation;
        scene = SceneManager.GetActiveScene();
        shouldReveal = true;
        persistantSceneObject = GameObject.Find("persistancescene");
        gameManagerScript = persistantSceneObject.GetComponent<GameManager>();

    }

    public void ChangeSceneOnClick()//changes scene if button pressed
    {            
            SceneManager.LoadScene(MenuLevelStarter);    
        
            //If a button is pressed then the player has entered through main menu therefore load first level
    }
    void OnTriggerEnter(Collider other)//changes scene if player interacts with a door (an object with a trigger)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log(gameManagerScript.BattleScene);
            if (gameManagerScript.BattleScene == true) //SECTION FOR ONLY BATTLE SCENES   
            {
                if (gameObject.CompareTag("2.5Enemy"))
                {
                   // GameManager.TDEnemyCollision = gameObject; //gameManagerScript
                }
                GameManager.EnemyToBattleName = gameObject.name;
                
                //Assigned to make it public for use in GameManager (transfer between scenes)
            }
            

            //MAYBE ANOTHER IF SHOULD GO HERE FOR DOORS? D O O R S S ! ! !

            Debug.Log(gameObject.name + "f");
            shouldReveal = !shouldReveal;
            // if (Fade_animator != null)
            //{
            Rigidbody PlayerRigidBody = other.GetComponent<Rigidbody>();
            PlayerRigidBody.constraints = RigidbodyConstraints.FreezePosition;
            //     //Fade_animator.SetTrigger("FadeOut"); Always commented

            
            Fade_animator.SetBool("bFadeOut", true);

            //Animation to fade into level
            // }
            // else
            //  {
                // Fade_animator.SetBool("bFadeOut", false);
                   //OnFadeComplete();
            //Once the fade animation is complete level is loaded
            //  }  
            ///////////////////////////////////////////////////////////////////////////////////////////
            //  if (!transitionStarted)
            //  {
            //      transitionStarted = true;
            //  }
            // if (transitionStarted)
            // {
            //      transitionProgress += Time.deltaTime * transitionSpeed;
            //     blackScreen.material.SetFloat("_CutOff", transitionProgress);
            //      if(transitionProgress >= 1f)
            //     {
            //         transitionStarted = false;
            //        SceneManager.LoadScene(levelToLoad);
            //        transitionProgress -= Time.deltaTime * transitionSpeed;
            //        blackScreen.material.SetFloat("_CutOff", transitionProgress);

            //     }
            //   }
            //    if (!transitionStarted)
            //    {

            //   }
        }
    }
    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //      shouldReveal = !shouldReveal;
        // }
        scene = SceneManager.GetActiveScene();
        if (shouldReveal)
        {
            Fade_animator.SetBool("bFadeOut", false);
            if (blackScreen != null)
            {
                blackScreen.material.SetFloat("_CutOff", Mathf.MoveTowards(blackScreen.material.GetFloat("_CutOff"), -0.1f, transitionSpeed * Time.deltaTime));
            }
        }
        else
        {
            if (blackScreen != null)
            {
           //     Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50, Time.deltaTime * 5);
          //  if (Camera.main.fieldOfView > 50 && Camera.main.fieldOfView < 51)
           // {
                
                blackScreen.material.SetFloat("_CutOff", Mathf.MoveTowards(blackScreen.material.GetFloat("_CutOff"), 1.1f, transitionSpeed * Time.deltaTime));
         //   }

            
                if (blackScreen.material.GetFloat("_CutOff") == 1.1f)
                {
                    Debug.Log("SCENE CHANGE !");
                    // GameManager.spawnLocation = Player.transform.position;
                    //gameManagerScript.spawnLocation = new Vector3(0, 1000, 0);
                    GameManager.previousPosition = Player.transform.position;
                    SceneManager.LoadScene(levelToLoad);
                    //gameManagerScript.spawnLocation = new Vector3 (0, 100 ,0);
                   // GameManager.Instance.spawnLocation = new Vector3(0, 100, 0);

                    //  if (scene.name == levelToLoad)
                    //  {
                    Debug.Log(scene.name);
                    if (scene.name.Contains("BattleGround"))
                    {

                        Debug.Log("woakh");
                      //  Instantiate(gameManagerScript.EnemyToSpawn, new Vector2(0, 0), Quaternion.identity);
                        // gameManagerScript.SpawnEnemies();


                    }
                }
            }
        }
        if (BattleSceneComplete == true)
        {
            VictoryText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                BattleSceneComplete = false;
                //StartCoroutine(ExecuteAfterTime());
                RestorePosition = true;
                SceneManager.LoadScene("2.5D Test"); //USE TRANSITION BEFORE LOADING IN?
            }

            //transport to previous scene///////
            
            //Player.transform.position = new Vector3(0,1000,0);
            //if (scene.name == "2.5D Test")
           // {
            //    Player.transform.position = gameManagerScript.spawnLocation;
           // }

            
        }
       // IEnumerator ExecuteAfterTime()
      //  {
       //     yield return new WaitForSeconds(3); //MAYBE CHANGE TO MENU AND 'PRESS BUTTON TO CONTINUE'

            // Code to execute after the delay
          //  BattleSceneComplete = false;
           // Debug.Log("Should switch to 2.5d");
           // RestorePosition = true;
           // SceneManager.LoadScene("2.5D Test"); //USE TRANSITION BEFORE LOADING IN?
       // }
    }
    // public void OnFadeComplete()
    // {
    //     SceneManager.LoadScene(levelToLoad);
    //Loads the level to load into the game
    //   }

   

}
