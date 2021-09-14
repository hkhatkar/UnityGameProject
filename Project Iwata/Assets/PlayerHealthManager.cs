using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    NewPLATPlayerMovement PlayerMoveScript;
    [SerializeField]
    private Stat health;
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
        health.MyCurrentValue += 0;//REMOVE THIS LATER THIS IS JUST FOR DEBUGGING TO SEE HOW HEAL WORKS
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyBullet"))
        {//If the player collides with an enemy...


            // knockbackCount = knockbackLength;
            if (col.transform.position.x < transform.position.x)
            {
                //   knockFromRight = true;

            }
            else
            {
                //  knockFromRight = false;
            }
            //The player would be knocked back in the opposite direction they are facing
            if (!(col.contacts[0].otherCollider.transform.gameObject.name == "ShieldPivot"))//If the collision is not with the child object of player called ShieldPivot...
            {//if the player does not block the enemy in time...
                if (EnemyBullet.BulletDamageMultiplierGlobal == 0)
                {
                    EnemyBullet.BulletDamageMultiplierGlobal = 1;
                    //The player would take damage depending on the enemies multiplier
                }



                health.MyCurrentValue -= 5 * EnemyBullet.BulletDamageMultiplierGlobal;
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

            //// StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
           // StartCoroutine(cameraShake.ShakeCustom(0.2f, 1.2f));
            //This coroutine will be responsible for causing a camera shake when the player gets hit by enemy
        }
    }
}