using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicStat : MonoBehaviour
{
    private Image content;
    public float MyMaxMagicValue { get; set; }
    float currentValue;
    private float currentFill;
    public float InitializedHealth;
    public float MyCurrentValue
    {//This function is responsible for setting players max health and varying current health
        get
        {//Return the current health value
            return currentValue;
        }
        set
        {
            if (value > MyMaxMagicValue)
            {
                currentValue = MyMaxMagicValue;
            }//makes sure current value doesnt go above max value
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / MyMaxMagicValue;
        }
    }
    private void Start()
    {
        StartCoroutine(RegenerateMagic());
        content = GetComponent<Image>();
        Initialize(100f, 100f);
    }
    void Update()
    {
        content.fillAmount = currentFill;
        //The health bar will be constantly refilled as health changes
    }
    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxMagicValue = maxValue;
        MyCurrentValue = currentValue;
        //initializes the current value and max value to be displayed in UI
    }

    IEnumerator RegenerateMagic()
    {
        while (true) //loops forever, magic always regenerates over time
        {
            if (!Input.GetMouseButton(0))
            {
                MyCurrentValue++;
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }         
        }
    }
}
