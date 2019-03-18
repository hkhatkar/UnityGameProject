using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpeedPowerUp : MonoBehaviour
{
    
    PlatformPlayerMovement PlayerRunSpeed;



    void Start()
    {

        PlayerRunSpeed = GetComponent<PlatformPlayerMovement>();
    }

    // Start is called before the first frame update
    public void ConsumeSpeedPowerUp()
    {
     
        PlatformPlayerMovement.runSpeed = PlatformPlayerMovement.runSpeed + 10;

        Destroy(gameObject);
        


    }

}
