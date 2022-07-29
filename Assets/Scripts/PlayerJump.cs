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
            JUMP_J();
            jump = true;
            
        }
    }

    private void FixedUpdate()
    {
        //Landing Platform
        if (rigid.velocity.y < 0)
        {

            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1);
            if (rayHit.collider.tag == "Ground")
            {

                if (rayHit.distance < 3f)
                {

                    anim.SetBool("isJumping", false);
                }
            }
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