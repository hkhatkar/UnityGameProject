using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanBullet : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rbGroundBullet;

    // Start is called before the first frame update
    void Start()
    {
        rbGroundBullet = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        rbGroundBullet.velocity = Vector2.left * speed;
    }
}
