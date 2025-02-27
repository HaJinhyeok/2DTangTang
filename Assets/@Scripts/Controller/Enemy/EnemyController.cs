using UnityEngine;

public class EnemyController : BaseController, IDamageable
{
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;
    protected Animator _animator;
    protected float _speed = 1.5f;
    protected float _currentHp = 3;
    protected float _atk = 1;

    public string Tag { get; set; } = Define.EnemyTag;

    public Vector3 Center { get => _collider2D.bounds.center; }

    protected override void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.freezeRotation = true;
        _rigidbody2D.linearVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        PlayerController pc = ObjectManager.Instance.Player;
        // 일단 플레이어가 존재하는지 검사
        if (pc == null)
            return;

        Vector3 targetPos = pc.transform.position;
        Vector3 targetDir = (targetPos - transform.position).normalized;
        _rigidbody2D.MovePosition
            (transform.position + targetDir * _speed * Time.deltaTime);
        _spriteRenderer.flipX = (targetDir.x < 0);
        _animator.SetBool(Define.isMoveHash, (Vector2)targetDir != Vector2.zero);
    }

    public bool AnyDamage(float damage, GameObject damageCauser, Vector2 hitPoint = default)
    {
        _currentHp -= damage;
        if(_currentHp <= 0)
        {
            ObjectManager.Instance.Despawn(this);
        }
        OnKnockBack(damageCauser);
        return true;
    }

    void OnKnockBack(GameObject causer)
    {
        Debug.Log("KnockBack!!");
        Vector3 dir = (transform.position - causer.transform.position).normalized;
        _rigidbody2D.AddForce(dir * 5000f);
    }
}
