using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpPower = 1f;
    Rigidbody2D rigid;
    bool isJumping = false;
    Animator anim;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetTrigger("doJump");
            anim.SetBool("isJumping", true);
        }
    }
    private void FixedUpdate()
    {
        Jump();
    }
    void Jump()
    {
        if (!isJumping)
        {
            return;
        }
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8 && rigid.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
        }
    }
}
