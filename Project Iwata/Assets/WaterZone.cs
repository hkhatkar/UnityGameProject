using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
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
  //  void OnTriggerExit2D(Collider2D collision)
   // {
  //      PlatformPlayerMovement.runSpeed = PlatformPlayerMovement.runSpeed +10;
  //  }
}
