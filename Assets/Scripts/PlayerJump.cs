using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerJump : MonoBehaviour
{
    public float jumpPower;
    Rigidbody2D rigid;
    bool jump = false;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    // && !anim.GetBool("isJumping")
    public void JUMP_J()
    {
        //점프했을 때 
        if (jump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

        }

    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //JUMP_J();
            //jump = true;
            anim.SetBool("isJumping", true);
        }
    }
    //점프애니메이션
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
        }
    }
    //발판 통과 로직
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<CapsuleCollider2D>().isTrigger = false;
    }

    
}