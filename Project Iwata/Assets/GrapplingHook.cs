using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{//this script is used for managing the grappling hook ability
    public Rigidbody PlayerRB;
    public LineRenderer line;
    ConfigurableJoint joint;
    Vector3 targetPos;
    RaycastHit hit;
    public float distance = 15f;
    public LayerMask mask;
    float step = 0.1f;
    float d;
    Ray ray;
    Vector3 HookPosition;

    float currentTime = 0f;
    float timeToMove = 5f;
    NewPLATPlayerMovement PlayerScript;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = gameObject.GetComponent<NewPLATPlayerMovement>();
        line.enabled = false;
    }


    void Update()
    {
        if (line.enabled == true)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, HookPosition);
        }
      
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {//When shift is pressed these checks would be done...

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Hook"))
            {//check to see if the raycast shot from the player to mouse position hits an object named hook

                Debug.Log("in");
                AbilityManager.AbilityInUse = true;
                line.enabled = true;
                line.SetPosition(0, transform.position);
                Transform HookTransform = hit.collider.gameObject.transform.Find("HookPosition"); //HOOK NAME MUST ALWAYS BE "HookPosition"
                HookPosition = HookTransform.position;
                line.SetPosition(1, HookPosition); 
                //if so hook is assigned to HookTransform

            }  
        }

        if (AbilityManager.AbilityInUse == true)
        {//If shift is still being held and the hook is identified then it will move the player towards that object
            if (currentTime <= timeToMove)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, HookPosition, currentTime / timeToMove);
            }
            else
            {
                currentTime = 0f;
            }
        }
        else
        {
            currentTime = 0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {//if the shift key is released then the player will no longer be lifted towards the hook position
            line.enabled = false;
            AbilityManager.AbilityInUse = false;
        }
        PlayerScript.HookManager(AbilityManager.AbilityInUse);
    }
    void OnDrawGizmos()
    {//This procedure is used as a debugging tool in order to allow programmers to physically see raycast drawn on the screen
        Gizmos.color = Color.blue;//Colour of this raycast is set to blue
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * transform.localScale.x * distance);
    }//end procedure
}
