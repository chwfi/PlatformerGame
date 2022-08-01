using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _damage = 3;
    private Transform _attackPos;
    private Vector2 _attackSize = Vector2.zero;
    

    private Vector2 _attack1Pos = new Vector2(0.7f, 0f);
    private Vector2 _attack2Pos = new Vector2(0.3f, -10f);
    private Vector2 _attack3Pos = new Vector2(0.1f, 0f);

    private Vector2 _attack1Size = new Vector2(2.05f, 0f);
    private Vector2 _attack2Size = new Vector2(2.5f, 0f);
    private Vector2 _attack3Size = new Vector2(3.4f, 0f);

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _attackPos = transform.Find("AttackPos");
    }

    public void Attack()
    {
        bool isAttack1 = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_01");
        bool isAttack2 = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_02");
        bool isAttack3 = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_03");

        if (isAttack1 == false && isAttack2 == false && isAttack3 == false)
        {
            _animator.SetTrigger("attack");
            _animator.SetBool("attack_2", false);
            _animator.SetBool("attack_3", false);
        }
        else if (isAttack1 == true)
        {
            _animator.SetBool("attack_2", true);
        }

        if (isAttack2 == false)
        {
            _animator.SetBool("attack_3", false);
        }
        else if (isAttack2 == true)
        {
            _animator.SetBool("attack_3", true);
        }
    }

    public void Attack_1()
    {
        
        _attackPos.position = _attack1Pos;
        _attackSize = _attack1Size;

        Attacking();
    }

    public void Attack_2()
    {
        _attackPos.position = _attack2Pos;
        _attackSize = _attack2Size;

        Attacking();
    }

    public void Attack_3()
    {
        _attackPos.position = _attack3Pos;
        _attackSize = _attack3Size;

        Attacking();
    }

    void Attacking()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(_attackPos.transform.position, _attackSize, 0, _enemyLayerMask);

        foreach (Collider2D enemy in enemies)
        {
            //enemy.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }
}
