using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    Vector2 moveInput;
    Rigidbody2D rigid;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 5;
    Animator anim;
    CapsuleCollider2D coll;
    
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        
        
    }
    private void Update()
    {
        Run();
        FlipSprite();
        
    }
    

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }
    void OnJump(InputValue value)
    {
        if (!coll.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {

            rigid.velocity += new Vector2(0f, jumpSpeed);



        }

    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, rigid.velocity.y);
        rigid.velocity = playerVelocity;

        bool playerHasSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", playerHasSpeed);
    }
    void FlipSprite()
    {
        bool playerHasSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        if (playerHasSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigid.velocity.x), 1f);
        }


    }
    
}