using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Swing : MonoBehaviour
{
    public GameObject Player;
    float rotSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);

       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("Player"))
        {
            Player.transform.Rotate(0, 0, 1);
        }
    }
}
