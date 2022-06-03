using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{//This script is responsible for making custom objects, with custom properties such as movement, player effects and rotation
    NewPLATPlayerMovement player;
    PlayerHealthManager PHealth;
    public GameObject BreakBar;
    GameObject BreakBarInstance;
    bool DamageCoroutineActive = false;
    public int i = 0;
    [SerializeField]
    GameObject blood;
    [SerializeField]
    bool Spins, Moves, Flamable, DmgOnTouch, Bouncy, Climable, Breakable, RigidBody, isPlatform;
    

    public Vector3[] MovePoints;
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player =  (GameObject.FindGameObjectWithTag("Player")).GetComponent<NewPLATPlayerMovement>();
        PHealth = (GameObject.FindGameObjectWithTag("Player")).GetComponent<PlayerHealthManager>();
        if (Breakable)
        {//if an object is breakable it is assigned a bar of health to which it will be destroyed when it hits 0
            BreakBarInstance = Instantiate(BreakBar);
            BreakBarInstance.transform.parent = transform;

        }
    }
    void Update()
    {
        if (Spins) { ObjectSpins(); }
        if (Moves) { ObjectMoves(); }
        if (Flamable) { ObjectFlamable(); }
        if (DmgOnTouch) { ObjectDmgOnTouch(); }
        //if (Bouncy) { }
        if (Climable) { ObjectClimable(); }
        if (Breakable) { ObjectBreakable(); }
        if (RigidBody) { ObjectRigidBody(); }
        //all possible properties given to an object through unity interface
    }
    void ObjectSpins()
    {//rotates object at a defined centre
        Vector3 Centre = GetComponent<Collider>().bounds.center;
        transform.Rotate(new Vector3(0, 0, 1f));
    }

    void ObjectMoves()
    {//gives an array of points where the object will move from, repeating this loop
        bool positionReached = false;
        if (i < MovePoints.Length)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, MovePoints[i], MoveSpeed);
        }
        if (positionReached == false)
        {
            if (Vector3.Distance(gameObject.transform.position, MovePoints[i]) == 0)
            {
                positionReached = true;
                i++;
            }
        }
        if (i == MovePoints.Length)
        {
            //done / repeat or end
            i = 0;
        }
    }

    void ObjectFlamable()
    {
        //to be implemented
    }
    void ObjectDmgOnTouch()
    {
       //to be implemented
    }
   
    void ObjectClimable()
    {
        //to be implemented
    }
    void ObjectBreakable()
    {
        //to be implemented
    }
    void ObjectRigidBody()
    {
        //to be implemented
    }

     public void OnTriggerEnter(Collider col)
    {
        Debug.Log("col " + col.gameObject.name + " is it");
        if (isPlatform)
        {//if the object is a platform, then the player must move with the object if the player stands on it
            if (col.gameObject.name == "Player")
            {
                col.gameObject.transform.parent = transform;
            }//thi is done by making the player a parent of the object
        }
       
        if (Bouncy)
        {
            if (player.anim.GetBool("PLATx") == false)
            {
                player.HandleDodge(-25f);
            }
            else
            {
                player.HandleDodge(25f);
            }
        }//uses dodge behaviour as a rebound force from bouncy objects to cause a knockback
    }
    public void OnTriggerStay(Collider other)
    {
        if (DmgOnTouch)
        {//when an object stays within a dmgOnTouch property object, it will take damage overtime
            if (other.gameObject.name == "Player" && DamageCoroutineActive == false)
            {
                DamageCoroutineActive = true;
                StartCoroutine(DamageOverTime());
                
            }
        }
    }
    public void OnTriggerExit(Collider col)
    {//disables some properties when the player is outside the trigger
        if (isPlatform)
        {
            if (col.gameObject.name == "Player")
            {
                col.gameObject.transform.parent = null;
            }
        }
        if (DmgOnTouch)
        {
            if (col.gameObject.name == "Player")
            {
                DamageCoroutineActive = false;
            }
        }
    }
    IEnumerator DamageOverTime()
    {
        while (DamageCoroutineActive == true)
        {
            PHealth.health.MyCurrentValue -= 5;
            Instantiate(blood, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            
        }
    }
}


