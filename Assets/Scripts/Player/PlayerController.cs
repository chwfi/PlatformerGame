using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigid;
    private Animator _animator;
    private Ghost _ghost;

    [Header("Condition Property")]
    [SerializeField] private float hp = 20f;



    [Header("Move Property")]
    private KeyCode _leftKey = KeyCode.LeftArrow;
    private KeyCode _rightKey = KeyCode.RightArrow;
    [SerializeField] private float _speed = 5;

    [Header("Jump Property")]
    private KeyCode _jumpKey = KeyCode.C;
    [SerializeField] private float _jumpPower = 12;
    private float _checkRadius = 0.05f;
    [SerializeField] private LayerMask _layerMask;
    private Transform _pos;
    private int _jumpMaxCount = 2;
    private int _jumpCurrentCount;
    private bool _isGround;

    [Header("Dash Property")]
    private KeyCode _dashKey = KeyCode.Z;
    private bool _canDash = true;
    private bool _isDashing = false;
    [SerializeField] private float _dashIngPower = 20f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

    [Header("Attack Property")]
    private KeyCode _attackKey = KeyCode.X;
    private KeyCode _heavyattackKey = KeyCode.S;
    private PlayerAttack _playerAttack;
    private bool _isAttacking;
    public bool IsAttacking
    {
        get => _isAttacking;
        set => _isAttacking = value;
    }


    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _ghost = GetComponent<Ghost>();

        _playerAttack = GetComponent<PlayerAttack>();
        _pos = transform.Find("Pos");
    }


    private void FixedUpdate()
    {
        if (_isDashing == true)
        {
            return;
        }

        Moving();
    }

    private void Moving()
    {
        bool isLeftKey = Input.GetKey(_leftKey);
        bool isRightKey = Input.GetKey(_rightKey);

        if (isLeftKey == isRightKey)
        {
            _animator.SetBool("isRun", false);
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        }
        else if (isLeftKey == true)
        {
            _animator.SetBool("isRun", true);
            transform.localScale = new Vector2(-1, 1);
            _rigid.velocity = new Vector2(-_speed, _rigid.velocity.y);
        }
        else if (isRightKey == true)
        {
            _animator.SetBool("isRun", true);
            transform.localScale = new Vector2(1, 1);
            _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
        }
    }

    private void Update()
    {
        
        Attacking();

        HeavyAttacking();

        Jumping();

        Dashing();
    }


    private void Attacking()
    {
        bool isAttackKey = Input.GetKeyDown(_attackKey);

        if (isAttackKey == true)
        {
            _playerAttack.Attack();
        }
    }
    
    private void HeavyAttacking()
    {
        bool isHeavyAttackKey = Input.GetKeyDown(_heavyattackKey);

        if (isHeavyAttackKey == true)
        {
            _playerAttack.HeavyAttack();
        }
        while (isHeavyAttackKey == true)
        {
            _speed = 0;
        }
    }

    private void Jumping()
    {
        if (_isDashing == true)
        {
            return;
        }

        _isGround = Physics2D.OverlapCircle(_pos.position, _checkRadius, _layerMask);
        bool isJumpkey = Input.GetKeyDown(_jumpKey);

        _animator.SetFloat("yVelocity", _rigid.velocity.y);

        if (_isGround == true && isJumpkey == true && _jumpCurrentCount > -1)
        {
            _animator.SetBool("isJump", true);
            _animator.SetTrigger("Jump");
            _rigid.velocity = Vector2.up * _jumpPower;
        }
        if (_isGround == false && isJumpkey == true && _jumpCurrentCount > 0)
        {
            _animator.SetBool("isJump", true);
            _animator.SetTrigger("Jump");
            _rigid.velocity = Vector2.up * _jumpPower;
        }

        if (Input.GetKeyUp(_jumpKey))
        {
            _jumpCurrentCount--;
        }

        if (_isGround == true)
        {
            _jumpCurrentCount = _jumpMaxCount;
            _animator.SetBool("isJump", false);
        }
    }

    private void Dashing()
    {
        //bool isDashKey = Input.GetKeyDown(_dashKey);

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash == true)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = _rigid.gravityScale;
        _rigid.gravityScale = 0;
        _rigid.velocity = new Vector2(transform.localScale.x * _dashIngPower, 0f);
        _ghost.MakeGhost = true;
        _animator.SetBool("isRun", true);
        yield return new WaitForSeconds(_dashingTime);
        _rigid.gravityScale = originalGravity;
        _isDashing = false;
        _ghost.MakeGhost = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;

    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        _animator.SetTrigger("hit");

        if (hp <= 0)
        {
            Debug.Log("Death");
        }
    }
}

