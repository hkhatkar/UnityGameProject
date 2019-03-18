using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    static public bool playDodgeAnim = false;
    public Animator animator;
    private Rigidbody2D rb;
    public float DodgeSpeed;
    private float DodgeTime;
    public float startDodgeTime;
    private int direction;

    public int CooldownTime;
    private float nextUseTime = 0;

    //public Vector3 SwitchDirection;//SWITCHES THE WAY OF ROLL
   // public Transform PlayersTransform;//USED FOR SWITCHING DIRECTION OF ROLL
    // Start is called before the first frame update
    void Start()
    {
     //   SwitchDirection = PlayersTransform.localScale;
        rb = GetComponent<Rigidbody2D>();
        DodgeTime = startDodgeTime;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(direction == 0)
            //Didnt put Shift in above if statement because if the player is walking before pressing Shift, dodge will not happen
        {
            playDodgeAnim = false;
            if (Time.time > nextUseTime)
            {
                if (animator.GetBool("PLATx") == true && Input.GetKeyDown(KeyCode.LeftShift)) // left
                {
                    // SwitchDirection.z *= -1;
                    // PlayersTransform.localScale = SwitchDirection;
                    direction = 1;
                }
                else if (animator.GetBool("PLATx") == false && Input.GetKeyDown(KeyCode.LeftShift)) // right
                {
                    direction = 2;
                }
            }
        }
        else
        {
            if(DodgeTime <= 0)
            {
                direction = 0;
                DodgeTime = startDodgeTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                DodgeTime -= Time.deltaTime;
                if(direction == 2)
                {
                    playDodgeAnim = true;
                    rb.velocity = Vector2.right * DodgeSpeed;
                    nextUseTime = Time.time + CooldownTime;
                }
                else if (direction == 1)
                {
                    playDodgeAnim = true;
                    rb.velocity = Vector2.left * DodgeSpeed;
                    nextUseTime = Time.time + CooldownTime;
                }
            }
        }
        
    }
}
