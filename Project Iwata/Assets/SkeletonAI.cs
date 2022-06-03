using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{//NON ACTIVE SCRIPT

    public float speed;//speed
    public float stoppingDistance;//enemy stops getting closer
    public float retreatDistance;//when the enemy backs away

    public Transform player;
    public Rigidbody2D rbSkeleton;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);//towards player

        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position)>retreatDistance)
        {
            transform.position = this.transform.position;//still
        }
        else if(Vector2.Distance(transform.position, player.position)< retreatDistance)//away from player
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        
    }
}
