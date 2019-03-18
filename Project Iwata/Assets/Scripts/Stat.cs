using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stat : MonoBehaviour {
    private GameMaster gm;

    private Image content;

    public GameObject PlayerObject;

    [SerializeField]
    private Text statValue;

    private float currentFill;
    public static float MyMaxValue { get; set; }// was not static

    public static float currentValue;// was private + not static
    bool Died = false;
   

    public float MyCurrentValue
    {
        get
        {
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
            }
            else
            {
                currentValue = value;
            }
            //currentValue = value;

            currentFill = currentValue / MyMaxValue;


            statValue.text = currentValue + " / " + MyMaxValue;

            if (currentValue == 0)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               // Application.LoadLevel("PIV2"); //change later
            }//WHEN HEALTH IS EQUAL TO 0 THEN IT WILL RESPAWN TO MAIN AREA / MAP

        }
}
   



    // Use this for initialization
    void Start () {

        

        content = GetComponent<Image>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (Died == true)
        {
            PlayerObject.transform.position = gm.LastCheckPointPos;
            Died = false;
        }
        else
        {
         //   PlayerObject.transform.position ;
        }


    }
	
	// Update is called once per frame
	void Update () {

        content.fillAmount = currentFill;
	}

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;


    }
}
