using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HunterMovement : MonoBehaviour
{
  
    [SerializeField]
    float move;
    [SerializeField] private const float JumpForce = 100f;
    private Vector3 Velocity = Vector3.zero;
    public Rigidbody2D HunterRigidbody2D;
    [Range(0, .3f)] [SerializeField] private float MoveSmoothing = .05f;
    public Transform HunterTransform;
    public Animator HunterAnimator;
    [SerializeField] private Transform GroundCheck;
    public bool Grounded;            // Whether or not the player is grounded.
    const float GroundedRadius = .2f;
    [SerializeField] private LayerMask WhatIsGround;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    // Start is called before the first frame update
    void Start()
    {
        
        move = 0.7f;

    }
    private void Awake()
    {
        
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void FixedUpdate()
    {

        

        bool wasGrounded = Grounded;
         Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {//if the pause menu isnt paused and A is entered
            HunterAnimator.SetBool("Moving", true);
            HunterAnimator.SetBool("FacingBack", true);

            Vector3 targetVelocity = new Vector2(-move * 10f, HunterRigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            HunterRigidbody2D.velocity = Vector3.SmoothDamp(HunterRigidbody2D.velocity, targetVelocity, ref Velocity, MoveSmoothing);

            Debug.Log("L");
            //Player moves left
           

        }
        
        else if (Input.GetKey(KeyCode.D))
        {//if the pause menu isnt paused and D is entered
            HunterAnimator.SetBool("Moving", true);
            HunterAnimator.SetBool("FacingBack", false);

            Vector3 targetVelocity = new Vector2(move * 10f, HunterRigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            HunterRigidbody2D.velocity = Vector3.SmoothDamp(HunterRigidbody2D.velocity, targetVelocity, ref Velocity, MoveSmoothing);

            Debug.Log("R");
            //Player moves right
            

        }
        if (Input.GetKeyUp(KeyCode.A))
        {//if the pause menu isnt paused and A is entered
            HunterAnimator.SetBool("Moving", false);
            HunterRigidbody2D.velocity = Vector2.zero;

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            HunterAnimator.SetBool("Moving", false);
            HunterAnimator.SetBool("FacingBack", false);
            HunterRigidbody2D.velocity = Vector2.zero;

        }
        if (Grounded && Input.GetKey(KeyCode.Space))
        {
            // Add a vertical force to the player.
            Grounded = false;
            HunterAnimator.SetBool("Jumping", true);
            HunterRigidbody2D.AddForce(new Vector2(0f, JumpForce));// * Time.fixedDeltaTime));
        }
    }
    public void OnLanding()
    {//procedure for when the player lands back on the ground after jumping
        Grounded = true;
        HunterAnimator.SetBool("Jumping", false);
    }

}
