using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumpCooldown : MonoBehaviour {
//NON ACTIVE SCRIPT
    public float cooldownTime = 2;
    private float nextFireTime = 0;
    private void Update()
    {

        if (Time.time > nextFireTime)
        {
            if (Input.GetButtonDown("Jump"))
            {
                print("jump used cooldown started");
                nextFireTime = Time.time + cooldownTime;

            }
        }
    }
}
