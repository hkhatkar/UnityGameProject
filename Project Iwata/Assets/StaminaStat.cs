using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaStat : MonoBehaviour
{//this script is responsible for managing the stamina bar for when the player runs. When it runs out, the player must wait to run again
    private Image content;
    public float MyMaxMagicValue { get; set; }
    float currentValue;
    private float currentFill;
    public float InitializedHealth;
    NewPLATPlayerMovement playerscript;
    Image UIFill, UIBackground;

    public float MyCurrentValue
    {//This function is responsible for setting players max health and varying current health
        get
        {//Return the current health value
            return currentValue;
        }
        set
        {
            if (value >= MyMaxMagicValue)
            {
                currentValue = MyMaxMagicValue;
                UIFill.enabled = false;
                UIBackground.enabled = false;

            }//makes sure current value doesnt go above max value
            else if (value < 0)
            {
                currentValue = 0;
                playerscript.anim.SetBool("PLATSprinting", false);
                playerscript.SprintCoolDown = true;
                StartCoroutine(SprintCoolDownTimer());
            }
            else
            {
                currentValue = value;
                UIFill.enabled = true;
                UIBackground.enabled = true;
            }
            currentFill = currentValue / MyMaxMagicValue;
        }
    }
    private void Start()
    {
        UIFill = gameObject.GetComponent<Image>();
        UIBackground = transform.parent.GetComponentInParent<Image>();
       
        playerscript = GameObject.FindObjectOfType<NewPLATPlayerMovement>();
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
        while (true) //loops forever
        {
            if (Input.GetKey(KeyCode.LeftControl) && !playerscript.SprintCoolDown)
            {
                MyCurrentValue--;
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                MyCurrentValue++;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    IEnumerator SprintCoolDownTimer()
    {
        yield return new WaitForSeconds(2f);
        playerscript.SprintCoolDown = false;
    }
}
