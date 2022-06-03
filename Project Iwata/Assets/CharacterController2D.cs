using UnityEngine;
using UnityEngine.Events;



public class CharacterController2D : MonoBehaviour
{//NON ACTIVE SCRIPT
//This class is responsible for the character's movement functions such as jumping, moving and crouching/Aim mode

    private AirShot AirShotScript;//AIR SHOT
    [SerializeField] private Transform CeilingCheck;                          // This is the position of the ceiling detector
    [SerializeField] private Collider2D CrouchDisableCollider;                // A collider that will be disabled when crouching to reduce height
    [SerializeField] private const float JumpForce = 500f;                          // The force added vertically to player when they jump
    [SerializeField] private bool AirControl = false;                         // Whether or not a player can control their direction whilst jumping
    [SerializeField] private LayerMask WhatIsGround;                          // A variable that informs the ground check what tagd are considered to be labelled as "ground"
    [SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is on ground


    [Range(0, 1)] [SerializeField] private float CrouchSpeed = 0f;          // maximum speed applied to player whilst crouching (or in Aiming state)
    [Range(0, .3f)] [SerializeField] private float MoveSmoothing = .05f;  // Determines the stopping time of the player after the user has entered an movement input

    const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool Grounded;            // Whether or not the player is grounded.
    const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody playerRigidbody2D;
    public static bool FacingRight = true;  // For determining which way the player is currently facing.
    //This is used within the lift object class aswell
    private Vector3 Velocity = Vector3.zero;
    float JumpCount = 0;
    float jumpCooldown;
    bool MaxJumped = false;
    

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public BoolEvent OnCrouchEvent;

    private bool m_wasCrouching = false;

    void Start()
    {
        AirShotScript = GameObject.FindObjectOfType<AirShot>();
    }
    
    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

    }
    private void Update()
    {

        if (Grounded && Input.GetKey(KeyCode.Space)) //&& JumpCount < 2)
        {
             playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x, 0, 0);
            playerRigidbody2D.velocity = Vector3.up * 3111f * Time.deltaTime;
            Grounded = false;
            JumpCount++;


        }
        else if (Grounded == false && Input.GetKeyDown(KeyCode.Space) && MaxJumped == false)//Input.GetKeyDown(KeyCode.Space) 
        {

              playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x*1.37f, 0, 0);
            playerRigidbody2D.AddForce(Vector3.up * 555f);
            JumpCount = 0;
            MaxJumped = true;
        }
    }

    private void FixedUpdate()
    {
    
        AirShotScript.checkIfInAir(Grounded);//AIRSHOT

        bool wasGrounded = Grounded;
        Grounded = false;
        
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground

        Collider[] colliders = Physics.OverlapSphere(GroundCheck.position, GroundedRadius, WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                JumpCount = 0;
                jumpCooldown = Time.time + 0.2f;
                MaxJumped = false;

                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
            else if (Time.time < jumpCooldown)
            {
                Grounded = true;
            }
            else { Grounded = false; }

            }
        
    }

    public void Move(float move, bool crouch, bool jump)
    {
        float runMultiplier = 8f;
        // If crouching (Aim mode), check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, WhatIsGround))
            {
                crouch = true;
            }
        }
        //only control the player if grounded or airControl is turned on

        if (Grounded || AirControl)
        {
            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }
                // Reduce the speed by the crouchSpeed multiplier. Multiplier will be 0 however this could be altered later.
                move *= CrouchSpeed;
                // Disable one of the colliders when crouching
                if (CrouchDisableCollider != null)
                    CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (CrouchDisableCollider != null)
                    CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                runMultiplier = 13f;
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector3(move * runMultiplier, playerRigidbody2D.velocity.y);//WALK SPEED !

            // And then smoothing it out and applying it to the character
            playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref Velocity, MoveSmoothing);

        }
    }

    
}