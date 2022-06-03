using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{//NON ACTIVE SCRIPT
    NewPLATPlayerMovement PlayerMoveScript;
    [SerializeField]
    public Stat health;
    [SerializeField]
    private float initialHealth;
    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMoveScript = gameObject.GetComponent<NewPLATPlayerMovement>();
        health.Initialize(initialHealth, initialHealth);
        //Initializes the players health to full health at the start of procedure
        //This is linked to another separate class
        health.MyCurrentValue += 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyBullet"))
        {//If the player collides with an enemy...

            if (!(col.contacts[0].otherCollider.transform.gameObject.name == "ShieldPivot"))
            //If the collision is not with the child object of player called ShieldPivot...
            {
                health.MyCurrentValue -= 5;//* EnemyBullet.BulletDamageMultiplierGlobal;
                Instantiate(blood, transform.position, Quaternion.identity);
                CinemachineShake.Instance.ShakeCamera(4f, 0.3f);


                if (col.gameObject.transform.position.x >= gameObject.transform.position.x)
                {
                    PlayerMoveScript.HandleDodge(-20f);
                }
                else
                {
                    PlayerMoveScript.HandleDodge(20f);
                }
                //The player's health is deducted by 5*multiplier of enemy

            }
            else
            {
                EnemyBullet.BulletDamageMultiplierGlobal = 0;
            }
        }
    }
}