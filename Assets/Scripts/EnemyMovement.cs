using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D flipCollider;

    [SerializeField] float moveSpeed = 1f;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        flipCollider = GetComponent<BoxCollider2D>();
        
    }

 
    void Update()
    {
        myRigidBody.velocity = new Vector2 (moveSpeed, 0f);
        myAnimator.SetBool("isRunning", true);
    }

    void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipSprite();

    }

    void FlipSprite(){
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody.velocity.x)), 1);
    }
}
