using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilityManager : MonoBehaviour
{

    public Image Ability1, Ability2, Ability3, Ability4;
    public Sprite CurrentSelectUI;
    public Sprite NotSelectedUI;
    Image previousChange;
    Component DodgeScript;
    //We need Grapple script, Pickup script, shield script
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
       if(Input.GetKeyDown(KeyCode.Alpha1)) //|| (Input.GetMouseButtonDown(1)  && count == 1) && AbilityInUse == false)
        {
            Ability1.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = false;
            gameObject.GetComponent<GrapplingHook>().enabled = false;
            gameObject.GetComponent<LiftObject>().enabled = true;
            Weapon.ShieldSelected = false;
            //  gameObject.GetComponentInChildren<Si>

            Ability2.sprite = NotSelectedUI;
            Ability3.sprite = NotSelectedUI;
            Ability4.sprite = NotSelectedUI;

            count =2;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))// || (Input.GetMouseButtonDown(1) && count == 2) && AbilityInUse == false)
        {
            Ability2.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = false;
            gameObject.GetComponent<GrapplingHook>().enabled = false;
            gameObject.GetComponent<LiftObject>().enabled = false;
            Weapon.ShieldSelected = true;

            Ability1.sprite = NotSelectedUI;
            Ability3.sprite = NotSelectedUI;
            Ability4.sprite = NotSelectedUI;

            count = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //|| (Input.GetMouseButtonDown(1) && count == 3) && AbilityInUse == false)
        {
            Ability3.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = true;
            gameObject.GetComponent<GrapplingHook>().enabled = false;
            gameObject.GetComponent<LiftObject>().enabled = false;
            Weapon.ShieldSelected = false;

            Ability2.sprite = NotSelectedUI;
            Ability1.sprite = NotSelectedUI;
            Ability4.sprite = NotSelectedUI;

            count = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //|| (Input.GetMouseButtonDown(1) && count == 4) && AbilityInUse == false)
        {
            Ability4.sprite = CurrentSelectUI;
            gameObject.GetComponent<Dodge>().enabled = false;
            gameObject.GetComponent<GrapplingHook>().enabled = true;
            gameObject.GetComponent<LiftObject>().enabled = false;
            Weapon.ShieldSelected = false;

            Ability2.sprite = NotSelectedUI;
            Ability3.sprite = NotSelectedUI;
            Ability1.sprite = NotSelectedUI;

            count = 1;
        }
    }
}
