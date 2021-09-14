using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
           
            playerscript.anim.SetBool("PLATMelee", true);
           
            if (playerscript.anim.GetBool("PLATx") == false)
            {
                Instantiate(SlashParticle, attackPoint);
                hitEnemies = Physics.OverlapSphere(attackPoint.position + new Vector3(1, 0, 0), attackRange, enemyLayers);
                playerscript.anim.Play("PLATMeleeLeft");
            }
            else
            {
                
                Instantiate(SlashParticle, attackPoint.position, Quaternion.Euler(new Vector3(0, -135, 0)));
                hitEnemies = Physics.OverlapSphere(attackPoint.position + new Vector3(-1, 0, 0), attackRange, enemyLayers);
                playerscript.anim.Play("PLATMeleeRight");
            }

            
            foreach(Collider enemy in hitEnemies)
            {
               
                enemyHealth = enemy.GetComponent<EnemyHealth>(); //get component in parent only picks some stuff, same with this i think (yes)

                Debug.Log("We hit " + enemy.name);
                if (enemyHealth.Enemyhealth.MyCurrentValue <= 0.2f)
                {
                      Time.timeScale = 0.2f;
                    FinisherUI.SetActive(true);
                    MagicScript.MyCurrentValue = 100f;
                   // CinemachineShake.Instance.ShakeCamera(8f, 0.3f);

                    //   playerscript.anim.Play("bug");
                }
                enemyHealth.Enemyhealth.MyCurrentValue -= 0.2f;
            }
        }
        playerscript.anim.SetBool("PLATMelee", false);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position + new Vector3 (0,0,0), attackRange);
        //-2 for left, 1 for right-----------------------------------^
    }
}
