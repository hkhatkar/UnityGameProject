using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJumpParticle : MonoBehaviour
{
    [SerializeField]
    GameObject dustCloud;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
    }
}
