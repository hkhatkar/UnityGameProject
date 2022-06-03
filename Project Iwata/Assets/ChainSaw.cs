using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : MonoBehaviour
{//NON ACTIVE SCRIPT
    Vector3 Centre;
   // Stat health;
    // Start is called before the first frame update
    void Start()
    {

        Centre = GetComponent<Collider>().bounds.center;
    }

    void Update()
    {
      //  transform.RotateAround(Centre, new Vector3(0,0,1f), 1f);
        transform.Rotate(new Vector3(0, 0, 1f));
    }

}
