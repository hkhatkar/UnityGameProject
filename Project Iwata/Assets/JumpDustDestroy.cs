using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDustDestroy : MonoBehaviour
{//NON ACTIVE SCRIPT
//destroys dust particle object on each jump

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
        //Destroys dust particles when jumping
    }

  
}
