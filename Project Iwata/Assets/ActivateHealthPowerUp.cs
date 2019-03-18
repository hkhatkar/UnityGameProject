using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHealthPowerUp : MonoBehaviour
{

    //public GameObject RemoveHealthPowerup;
    //  public Stat StatScript;

    // [SerializeField]
    // private Stat health;// make this equal to platformplayermovement health


    //  [SerializeField]
    //  private float initialHealth; // make this equal to platformplayermovement health

    PlatformPlayerMovement health;

    

    void Start()
    {
        // health.MyCurrentValue;
        health = GetComponent<PlatformPlayerMovement>();
    }

    // Start is called before the first frame update
    public void ConsumeHealthPowerUp()
    {
        // health.Initialize(initialHealth * 2, initialHealth * 2);
        // Debug.Log("THE BUTTON WAS PRESSED !?!?!?!?!?!?!??!?!?!??!???!?!?!?!??!??!?!?!?!??!?!?!?!?!??!?!?!?!??!?!?!?!??!?!?!?!");
         
        Stat.MyMaxValue = Stat.MyMaxValue + 25;
        Stat.currentValue = Stat.MyMaxValue;
        Destroy(gameObject);
        //Destroy(GameObject.Find("HealthButton(Clone)"));
  
        // RemoveHealthPowerup.SetActive(false);


    }

   
}
