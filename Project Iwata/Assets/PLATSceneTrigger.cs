using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PLATSceneTrigger : MonoBehaviour
{//This script is responsible for switching from 3d aspect scenes to side scrolling scenes, including battle scenarios
    public Animator Fade_animator;
    [SerializeField]//this prevents the programmer changing the data levelToLoad holds, as we want this only to change when a player sets a trigger by entering a door
    public static string levelToLoad; // this is set as public static, so it can be accessed by any class, this is needed because the scene that needs to be loaded changes depending on which event/ door has been triggered
    public string MenuLevelStarter; //same for levelToLoad but works only with main menu button

    //Shader transition vvv
    public Image blackScreen;
    public float transitionSpeed = 1f;

    public static bool shouldReveal;
    GameManager gameManagerScript;
    GameObject persistantSceneObject; // For GameManager script
    Scene scene;
    GameObject getPersistanceScene;
    public static bool BattleSceneComplete = false; // need to be static, only can be 1
    public GameObject Player;
    public static bool RestorePosition = false;
    public GameObject VictoryText;

    public void Start()
    {

        Player = GameObject.Find("Player");
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
               
                GameManager.EnemyToBattleName = gameObject.name;
                //Assigned to make it public for use in GameManager (transfer between scenes)
            }
            
            Debug.Log(gameObject.name);
            shouldReveal = !shouldReveal;
            Rigidbody PlayerRigidBody = other.GetComponent<Rigidbody>();
            PlayerRigidBody.constraints = RigidbodyConstraints.FreezePosition;
            Fade_animator.SetBool("bFadeOut", true);

        }
    }
    public void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (shouldReveal)
        {//manages the scene transition animation, fades out of scene
            Fade_animator.SetBool("bFadeOut", false);
            if (blackScreen != null)
            {
                blackScreen.material.SetFloat("_CutOff", Mathf.MoveTowards(blackScreen.material.GetFloat("_CutOff"), -0.1f, transitionSpeed * Time.deltaTime));
                //handles screen transition to fade out of scene
            }
        }
        else
        {
            if (blackScreen != null)
            {
                blackScreen.material.SetFloat("_CutOff", Mathf.MoveTowards(blackScreen.material.GetFloat("_CutOff"), 1.1f, transitionSpeed * Time.deltaTime));
                //fades into the second scene once loaded
            
                if (blackScreen.material.GetFloat("_CutOff") == 1.1f)
                {
                    Debug.Log("SCENE CHANGE");

                    GameManager.previousPosition = Player.transform.position;
                    SceneManager.LoadScene(levelToLoad);
                    Debug.Log(scene.name);
                    if (scene.name.Contains("BattleGround"))
                    {

                        Debug.Log("in");

                    }
                }
            }
        }
        if (BattleSceneComplete == true)
        {
            VictoryText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                BattleSceneComplete = false;
                RestorePosition = true;
                SceneManager.LoadScene("2.5D Test"); 
            }

          
            
        }
     
    }

}
