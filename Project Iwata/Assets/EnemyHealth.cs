using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{//This class is reponsible for the amount of health an enemy has. This could be varied depending on the enemy
    public GameObject EnemyObject;
    public GameObject CurrentEnemyHealthDisplay;
    public Transform CurrentEnemyHealthDisplayTransform;
    public float health;
    
    public float maxHealth;
    public GameObject blood;
    public Animator AnimateDeath;
    public BoxCollider2D InteractionArea; //The area in which the player could talk with the boss
    //declares variables to be assigned within unity
    public GameObject DroppedItem;//Boss will drop an item once defeated 
    public GameObject DroppedItemPos;//Position of dropped item
    public AudioSource HitSuccessfulAudio;
    private EnemyHealthBar EnemyHealthBarScript;
    public Transform HealthBarPlacement;

    [SerializeField]
    public EnemyHealthBar Enemyhealth;

    public GameObject CoinPrefabToDrop;

    public PLATSceneTrigger PLATSceneScript;

    public Bullet bulletScript;


    void Start()
    {
        bulletScript = GetComponent<Bullet>();
        EnemyHealthBarScript = GameObject.FindObjectOfType<EnemyHealthBar>();
        maxHealth = health;
        PLATSceneScript = GetComponent<PLATSceneTrigger>();


        Enemyhealth.Initialize(health, health);
        // EnemyHealthBarScript.UpdateHealthBar(health, maxHealth);

    }
    // Update is called once per frame
    void Update()
    {
       
        if (Enemyhealth.MyCurrentValue <= 0 )
        {//if the enemy health is less than or equal to 0
            if ((gameObject.CompareTag("NPC(Non interactable)") || gameObject.CompareTag("NPC") ))
            {
                if (AnimateDeath.GetBool("Defeated") == false)
                {
                    AnimateDeath.SetBool("Defeated", true);
                    if (DroppedItem != null)
                    {
                        Instantiate(DroppedItem, new Vector2(DroppedItemPos.transform.position.x, DroppedItemPos.transform.position.y), Quaternion.identity); //HERE
                    }
                    
                }
                
                gameObject.tag = ("NPC");               
                gameObject.GetComponent<InteractionObject>().enabled = true;
                InteractionArea.enabled = true;
               // Instantiate(BossDroppedItem, new Vector2(DroppedItemPos.transform.position.x, DroppedItemPos.transform.position.y), Quaternion.identity); //HERE


            }
            else if (gameObject.CompareTag("Enemy"))
            {
                Destroy(EnemyObject);
                if (DroppedItem != null)
                {
                    Instantiate(DroppedItem, new Vector2(DroppedItemPos.transform.position.x, DroppedItemPos.transform.position.y), Quaternion.identity); //HERE
                }
                else
                {
                    for (int i = 0; i < Random.Range(0, 6); i++) //RANDOM LOOT DROP -> MAKE THIS TO VARY DEPENDING ON ENEMY TYPE LATER? // I N E F F I C I E N T -> CAUSES DELAY SOMETIMES
                    {
                        Instantiate(CoinPrefabToDrop, new Vector2(DroppedItemPos.transform.position.x, DroppedItemPos.transform.position.y), Quaternion.identity); //HERE
                    }

                }
                
                
            }
            //Then enemy object is destroyed and therefore "dies"

        }


    }

    void OnTriggerEnter(Collider bulletcol)
    { 
        if (bulletcol.CompareTag("PlayerBullet"))
        {//when the player bullet collides with the enemy trigger...

            HitSuccessfulAudio.Play();
            
            //if the bullet hits the enemy with a weak shot (fast weapon)
                //health -= 0.1f; 
               //* EnemyBullet.BulletDamageMultiplierGlobal;
                //0.1 health is deducted from enemy

            
            //else if the bullet that hits the enemy is a strong shot (heavy weapon)

                // health -= 1;
                //1 health is deducted from enemy
                Enemyhealth.MyCurrentValue -= Bullet.DamageToDeal; //* EnemyBullet.BulletDamageMultiplierGlobal;

            
           // GameObject G = Instantiate(CurrentEnemyHealthDisplay);
           

            //CurrentEnemyHealthDisplay.transform.parent = gameObject.transform;
          //  EnemyHealthBarScript.UpdateHealthBar(health, maxHealth);
            Instantiate(blood, transform.position, Quaternion.identity);

            if (bulletcol.name.Contains("PLATBullet"))
            {
                CameraShake.bulletDestroyed = true;
            }
            Destroy(bulletcol.gameObject);
            //When the bullet collides with the enemy the bullet is destroyed and blood particles are displayed
        }

    }//end procedure
}//end class
