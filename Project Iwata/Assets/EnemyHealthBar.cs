using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{//This script is responsible for displaying the enemies health in UI form, representing this in a ratio bar

    private GameMaster gm;
    public Image content;
    private float currentFill = 1;
    public  float MyMaxValue { get; set; }
    public  float currentValue;
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
        }
    }

    void Update()
    {
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
