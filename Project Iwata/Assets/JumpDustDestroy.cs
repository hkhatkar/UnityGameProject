using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDustDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
        //Destroys dust particles when jumping
    }

  
}
