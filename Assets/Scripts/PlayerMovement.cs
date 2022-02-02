using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    [SerializeField] float playerSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject DeathTransitionSquare; 
    Animator myAnimator;
    BoxCollider2D feet;
    CapsuleCollider2D myCollider;
    CapsuleCollider2D enemyCollision;

    bool isAlive = true;
    float transitionTime = 5;

    float startGravity;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
        enemyCollision = Enemy.GetComponent<CapsuleCollider2D>();
        startGravity = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) 
        {
            while (transitionTime>0)
            {
                Color square = DeathTransitionSquare.GetComponent<SpriteRenderer>().color;

                square = Color.black;    
                square.a = transitionTime;
                Debug.Log(square.a);
                transitionTime-=1;
            }
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void FlipSprite()
    {
        bool isMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(isMoving)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1);
        }
    }

    

    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        

        bool isMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", isMoving);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        if(value.isPressed && feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity += new Vector2(0, jumpForce);
        }
    }

    

    

    void ClimbLadder()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Stairs")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = startGravity;
            return;
        }

        
        Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = playerVelocity;
        bool isMovingUp = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", isMovingUp);
        myRigidbody.gravityScale = 0;
      

        
    }

    void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            myRigidbody.gravityScale = 10;
            myRigidbody.velocity += new Vector2(-jumpForce*2, jumpForce);
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.22f);
            //Physics.IgnoreLayerCollision(8, 10);
            //Physics.IgnoreCollision(Enemy.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>());
        }
    }

    void TransitionToBlack(int opacity)
    {
        
    }
    
}
