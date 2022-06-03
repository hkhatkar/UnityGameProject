using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stat : MonoBehaviour {
 //Stat class is responsible for players health stat and the UI display of this
 //The health bar will show the proportion of health left once this reaches 0 the player will die

    private GameMaster gm;
    private Image content;
    public GameObject PlayerObject;
    [SerializeField]
    private Text statValue;
    private float currentFill;
    public static float MyMaxValue { get; set; }
    public static float currentValue;
    bool Died = false;
    //Declares variables

    public float MyCurrentValue
    {//This function is responsible for setting players max health and varying current health
        get
        {//Return the current health value
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }//makes sure current value doesnt go above max value
            else if (value < 0)
            {
                currentValue = 0;
                Died = true;
                //If the value of health is 0 then the player dies
            }
            else
            {
                currentValue = value;
            }//otherwise the current value will be equal to the new value when health is deducted          
            currentFill = currentValue / MyMaxValue;
            //current fill is the ration of health compared to the maximum

            statValue.text = currentValue + " / " + MyMaxValue;
            //Displays this value as text
            if (currentValue == 0)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }//WHEN HEALTH IS EQUAL TO 0 THEN IT WILL RESPAWN TO START OF MAP (resets the scene)
        }
}
    // Use this for initialization
    void Start () {
        content = GetComponent<Image>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //This is used to represent an UI image of the health bar
        //When the player dies the player will respawn at its last location saved in game master
        if (Died == true)
        {
            PlayerObject.transform.position = gm.LastCheckPointPos;
            Died = false;
            //Player respawns to last location
        }      
    }	
	// Update is called once per frame
	void Update () {
        content.fillAmount = currentFill;
        //The health bar will be constantly refilled as health changes
	}
    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        //initializes the current value and max value to be displayed in UI
    }
}
