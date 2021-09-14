using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
        // joint = GetComponent<ConfigurableJoint>();
        // joint.enabled = false;
        //line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //   if (Input.GetKey(KeyCode.CapsLock))//(joint.distance > 0.3f)     

        //   {

        //      joint.distance -= step;


        //}
        // else if (Input.GetKey(KeyCode.LeftControl))
        // { 
        //     joint.distance += step;
        //line.enabled = false;
        //  joint.enabled = false;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //  }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // targetPos = Camera.main.ScreenPointToRay(Input.mousePosition);
            //  targetPos.z = 0;
            // ray = new Ray(targetPos , Vector3.up);
           // if (AbilityManager.AbilityInUse == false)
        //    {
                

                // if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody>() != null && hit.collider.gameObject.CompareTag("Hook"))
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Hook"))
                {

                    // if (hit.collider.gameObject.CompareTag("Hook"))
                    // {
                    Debug.Log("innaroo");
                    AbilityManager.AbilityInUse = true;
                    // joint.enabled = true;
                    // joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    //  joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                    //   joint.distance = Vector2.Distance(transform.position, hit.point);

                    //  line.enabled = true;
                    // line.SetPosition(0, transform.position);
                    Transform HookTransform = hit.collider.gameObject.transform.Find("HookPosition"); //HOOK NAME MUST ALWAYS BE "HookPosition" !!!!!!!!!!!!!!!!
                    HookPosition = HookTransform.position;
                // line.SetPosition(1, HookPosition); //hit.point
                //   transform.position = Vector3.Lerp(transform.position, HookPosition, 3);
                // }
              
            }
          
            //   if (AbilityManager.AbilityInUse == true)
            //   {

            // PlayerRB.AddRelativeForce(HookPosition);
            //}
            //  }
            // else
            // {

            // }





        }

        if (AbilityManager.AbilityInUse == true)
        {
            if (currentTime <= timeToMove)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, HookPosition, currentTime / timeToMove);
            
            }
            else
            {
                //  transform.position = HookPosition;
                currentTime = 0f;
                //PlatformPlayerMovement.isClimbing = true;
                //transform.position = HookPosition;
            
            }
        }
        else
        {
            currentTime = 0f;
        }

        // if (Input.GetKey(KeyCode.A) && line.enabled == true)
        // {
        // PlayerRB.AddForce(new Vector2(-50f, 0f)); //Time.fixedDeltaTime
        // }
        //if (Input.GetKey(KeyCode.D) && line.enabled == true)
        //{
        // PlayerRB.AddForce(new Vector2(50f, 0f)); //Time.fixedDeltaTime
        //}


        //  if (Input.GetKey(KeyCode.LeftShift))
        //  {
        // line.SetPosition(0, HookPosition);
        // }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            
            //joint.enabled = false;
           // line.enabled = false;
            AbilityManager.AbilityInUse = false;
         

        }
       
    }
    void OnDrawGizmos()
    {//This procedure is used as a debugging tool in order to allow programmers to physically see raycast drawn on the screen
        Gizmos.color = Color.blue;//Colour of this raycast is set to blue
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * transform.localScale.x * distance);
    }//end procedure
}
