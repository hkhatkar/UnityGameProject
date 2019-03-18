using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    protected Animator myAnimator;
    protected Vector2 direction;

    private Rigidbody2D myRigidbody;
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Use this for initialization
   protected virtual void Start () {

        
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        direction = Vector2.up;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
            HandleLayers();

        
    }
    private void FixedUpdate()//fixed update is frame rate independant compared to normal update
    {
        Move();
    }
    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;
        //.normalized on direction is there to prevent the player moving faster when travelling diagonally
        
    }

    public void HandleLayers()
    {
        //handles animation layers

        
            if (IsMoving)
            {


                ActivateLayer("WalkLayer");

                //set walk animation to on

                myAnimator.SetFloat("x", direction.x);
                myAnimator.SetFloat("y", direction.y);
            }
            else
            {


                ActivateLayer("IdleLayer");
            }//else set the walk animation weight to 0 therefore it does not play walk animation

        
    }
    
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount;i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);

    }
}
