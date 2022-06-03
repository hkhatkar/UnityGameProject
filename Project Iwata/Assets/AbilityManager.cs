using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilityManager : MonoBehaviour
{//This script is used for managing the hot bar UI for abilities that use SHIFT (4 in total)
//including lifting objects, shield (still in progress), dodge and grappling to objects

    public Image Ability1, Ability2, Ability3, Ability4;
    public Sprite CurrentSelectUI;
    public Sprite NotSelectedUI;
    Image previousChange;
    Component DodgeScript;
    Component GrapplingScript;
    Component LiftScript;
    int count;
    public static bool AbilityInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Alpha1))
        {//If 1 is pressed, the first ability is selected (Lifting object)
            Ability1.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = false;
            gameObject.GetComponent<GrapplingHook>().enabled = false;
            gameObject.GetComponent<LiftObject>().enabled = true;
            Weapon.ShieldSelected = false;
            //All other  abilities are disabled

            Ability2.sprite = NotSelectedUI;
            Ability3.sprite = NotSelectedUI;
            Ability4.sprite = NotSelectedUI;
            //UI represents they are unselected

            count =2;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {//if 2 is pressed the second ability is selected (Shield)
            Ability2.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = false;
            gameObject.GetComponent<GrapplingHook>().enabled = false;
            gameObject.GetComponent<LiftObject>().enabled = false;
            Weapon.ShieldSelected = true;
            //All other  abilities are disabled

            Ability1.sprite = NotSelectedUI;
            Ability3.sprite = NotSelectedUI;
            Ability4.sprite = NotSelectedUI;
            //UI represents they are unselected

            count = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {//if 3 is pressed the third ability is selected (Dodging)
            Ability3.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = true;
            gameObject.GetComponent<GrapplingHook>().enabled = false;
            gameObject.GetComponent<LiftObject>().enabled = false;
            Weapon.ShieldSelected = false;
             //All other  abilities are disabled

            Ability2.sprite = NotSelectedUI;
            Ability1.sprite = NotSelectedUI;
            Ability4.sprite = NotSelectedUI;
            //UI represents they are unselected

            count = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {//if 4 is pressed the third ability is selected (Grappling hook)
            Ability4.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = false;
            gameObject.GetComponent<GrapplingHook>().enabled = true;
            gameObject.GetComponent<LiftObject>().enabled = false;
            Weapon.ShieldSelected = false;
            //All other  abilities are disabled

            Ability2.sprite = NotSelectedUI;
            Ability3.sprite = NotSelectedUI;
            Ability1.sprite = NotSelectedUI;
             //UI represents they are unselected
            count = 1;
        }
    }
}
