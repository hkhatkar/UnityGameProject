using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{//NON ACTIVE SCRIPT
    PlatformPlayerMovement PlayerRunSpeed;
    float OriginalSpeed;


    void Start()
    {

        PlayerRunSpeed = GetComponent<PlatformPlayerMovement>();
        OriginalSpeed = PlatformPlayerMovement.runSpeed;
    }

    // Update is called once per frame
  void OnTriggerEnter2D(Collider2D collision)
    {
        PlatformPlayerMovement.runSpeed = PlatformPlayerMovement.runSpeed -10;
    }

}
