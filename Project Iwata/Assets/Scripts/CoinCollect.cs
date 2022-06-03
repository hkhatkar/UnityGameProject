using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour {
//script that increments the the coin UI everytime the player enters the trigger area
    void OnTriggerEnter(Collider col)
    {
        ScoreTextScript.coinAmount++ ;
        Destroy(gameObject);
        //the object is destroyed after use
    }
}
