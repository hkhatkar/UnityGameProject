using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTextScript : MonoBehaviour {
//This script handles updating the UI element of the currency
    private TextMeshProUGUI text;
    public static int coinAmount;
	// Use this for initialization
	void Start () {

        text = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {

        text.text = coinAmount.ToString();
        //text is assigned to the amount of coins collected in string format
	}
}
