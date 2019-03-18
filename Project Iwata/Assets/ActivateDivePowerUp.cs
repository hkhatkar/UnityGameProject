using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDivePowerUp : MonoBehaviour
{
    PlatformPlayerMovement PlayerMass;
    Rigidbody2D PlayerRigidBody;

    void Start()
    {

       
    }

    // Start is called before the first frame update
    public void ConsumeDivePowerUp()
    {
        PlayerMass = GetComponent<PlatformPlayerMovement>();
        PlayerRigidBody = PlatformPlayerMovement.rb;
        PlayerRigidBody.mass = PlayerRigidBody.mass * 3;

        Destroy(gameObject);



    }


}
