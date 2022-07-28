using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerJump : MonoBehaviour
{
    bool isJumping = false;
    public float jumpPower;
    Rigidbody2D rigid;
    
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetTrigger("doJump");
            anim.SetBool("isJumping", true);
        }
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }
    private void FixedUpdate()
    {
        
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1);
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }
    
    
    
}
