using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{//this script is responsible for the melee attack ability

   public WeaponStyleManager getWeaponStyle;
    string meleeType;
    NewPLATPlayerMovement playerscript;
    MagicStat MagicScript;
    EnemyHealth enemyHealth;
    public float attackRange = 0.5f; //sphere radius
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public GameObject SlashParticle;
    public GameObject FinisherUI;
    Collider[] hitEnemies;
    // Start is called before the first frame update
    void Start()
    {
        MagicScript = GameObject.FindObjectOfType<MagicStat>();
        playerscript = gameObject.GetComponent<NewPLATPlayerMovement>();
        //we need magic script also because melee could trigger finishers, which fill in the magic bar
    }

    void Update()
    {

        useDagger();
        //currently there is only 1 type of melee weapon being the dagger method
 
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position + new Vector3 (0,0,0), attackRange);
    }//draws the raycast for debugging


    void useDagger()
    {
        if (Input.GetMouseButtonDown(1))
        {//checks if the user input right click

            playerscript.anim.SetBool("PLATMelee", true);
            if (playerscript.anim.GetBool("PLATx") == false)
            {//if the player is facing left
                Instantiate(SlashParticle, attackPoint);
                hitEnemies = Physics.OverlapSphere(attackPoint.position + new Vector3(1, 0, 0), attackRange, enemyLayers);
                playerscript.anim.Play("PLATMeleeLeft");
                //
            }
            else
            {//if the player is facing right
                Instantiate(SlashParticle, attackPoint.position, Quaternion.Euler(new Vector3(0, -135, 0)));
                hitEnemies = Physics.OverlapSphere(attackPoint.position + new Vector3(-1, 0, 0), attackRange, enemyLayers);
                playerscript.anim.Play("PLATMeleeRight");
            }


            foreach (Collider enemy in hitEnemies)
            {//If the player hits multiple enemies at once with its melee, we want to make sure they all get hit
            //therefore we collect these enemies in an array and deal damage to them separately

                enemyHealth = enemy.GetComponent<EnemyHealth>();

                Debug.Log("We hit " + enemy.name);
                if (enemyHealth.Enemyhealth.MyCurrentValue <= 0.2f)
                {
                    Time.timeScale = 0.2f;
                    FinisherUI.SetActive(true);
                    MagicScript.MyCurrentValue = 100f;
                }
                enemyHealth.Enemyhealth.MyCurrentValue -= 0.2f;
            }
        }
        playerscript.anim.SetBool("PLATMelee", false);
    }
}
