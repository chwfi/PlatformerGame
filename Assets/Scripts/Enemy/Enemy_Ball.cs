using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ball : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [Header("Attack Property")]

    [SerializeField] private float _damage;

    [SerializeField] private Transform _attackPos;
    [SerializeField] private Vector2 _attackSize = Vector2.zero;

    private Rigidbody2D _rigid;
    private Animator _animator;

    

    [Header("Move Property")]
    private bool _isMove = false;
    [SerializeField] protected float _speed;
    [SerializeField] private int _moveDir;
    public int MoveDir
    {
        get => _moveDir;
        set => _moveDir *= value;
    }
    Coroutine CoroutineMove;

    [Header("Find Player Property")]
    [SerializeField] private LayerMask _playerLayerMask;
    private bool _isPlayer;
    public Transform _player;

    [SerializeField] private Transform _pos;
    [SerializeField] private Vector2 _findRange;

    [Header("Death Property")]
    protected bool _isDeath = false;

    [Header("Attack Property")]
    private bool _isAttacking = false;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackDelay;
    Coroutine CoroutineAttack;
    
    public void Attack()
    {
        bool isAttack = _animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Ball_Attack");
        if (isAttack == false)
        {
            _animator.SetTrigger("attack");
        }
    }

    public void Attack_1()
    {
       // Attacking1();
    }

    /*void Attacking1()
    {
        Collider2D playerCol = Physics2D.OverlapBox(_attackPos.transform.position, _attackSize, 0, _playerLayerMask);

        if (playerCol != null)
        {
            playerCol.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }*/

    /*public void TakeDamage(int damage)
    {
        hp -= .1f;
        if (_isDeath == true)
        {
            return;
        }
        _animator.SetTrigger("hit");
        hp -= damage;
        StopAttack();

        

        if (hp <= 0)
        {
            // death
            _isDeath = true;
            StopMove();
            Death();
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            _animator.SetTrigger("death");

            Debug.Log($"{transform.name} Death");
        }
    }*/
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (_isDeath == true)
        {
            return;
        }
        _animator.SetTrigger("hit");

        if (currentHealth <= 0)
        {
            // death
            _isDeath = true;
            StopMove();
            Death();
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            _animator.SetTrigger("death");

            Debug.Log($"{transform.name} Death");
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _pos = transform.Find("Pos");
        _isMove = true;

        CoroutineMove = StartCoroutine(MonsterAI());

    }

    private void Update()
    {
        
        if (_isDeath == true)
        {
            return;
        }

        FindPlayer();
        //Attack();
        Chasing();
    }


    private void Chasing()
    {
        if (_isAttacking == false)
        {
            if (_isPlayer == true)
            {
                float distance = _player.position.x - transform.position.x;
                _moveDir = distance == 0 ? 0 : distance < 0 ? -1 : 1;

                if (Mathf.Abs(distance) < _attackRange)
                {

                    // attack
                    StartAttack();

                }
                else
                {
                    _isMove = true;
                }
            }
        }
    }

    protected void StartAttack()
    {
        CoroutineAttack = StartCoroutine(Attacking());
    }
    protected void StopAttack()
    {

        if (CoroutineAttack != null)
        {
            StopCoroutine(CoroutineAttack);
        }

        _isAttacking = false;
    }

    IEnumerator Attacking()
    {
        _isAttacking = true;
        _isMove = false;
        transform.localScale = new Vector2(_moveDir, 1);
        Attack();
        yield return new WaitForSeconds(_attackDelay);
        _isAttacking = false;
    }

    


    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_pos.position, _findRange);
    }*/

    void FindPlayer()
    {
        Collider2D player = Physics2D.OverlapBox(_pos.position, _findRange, 0, _playerLayerMask);

        if (player != null)
        {
            _player = player.transform;

            _isPlayer = true;

            StopMove();
        }
        else
        {
            if (_isPlayer == true)
            {
                _isPlayer = false;
                StartMove();
            }
        }
    }

    public void StartMove()
    {
        _isMove = true;
        CoroutineMove = StartCoroutine(MonsterAI());
    }
    public void StopMove()
    {
        _isMove = false;

        if (CoroutineMove != null)
        {
            StopCoroutine(CoroutineMove);
        }
    }

    IEnumerator MonsterAI()
    {
        _moveDir = Random.Range(-1, 2);
        int delay = Random.Range(2, 5);
        yield return new WaitForSeconds(delay);

        if (_isPlayer == false)
            StartMove();
    }

    private void FixedUpdate()
    {
        if (_isDeath == true)
        {
            return;
        }

        if (_isMove == false)
        {
            return;
        }

        if (_moveDir == 0)
        {
            _animator.SetBool("isRun", false);
        }
        else if (_moveDir == 1)
        {
            _animator.SetBool("isRun", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_moveDir == -1)
        {
            _animator.SetBool("isRun", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        _rigid.velocity = new Vector2(_moveDir, _rigid.velocity.y);
    }

    

    public void Death()
    {
        Debug.Log($"{transform.name} Destroy");

        Destroy(this.gameObject);
    }
}
