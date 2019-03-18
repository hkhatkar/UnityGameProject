using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        ScoreTextScript.coinAmount++ ;
        Destroy(gameObject);
    }
}
