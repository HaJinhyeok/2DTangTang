using UnityEngine;

public class PlayerController : BaseController
{
    private Vector2 _moveDir;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Collider2D _collider2D;
    private SkillSystem _skillSystem;

    public Vector3 Center { get => _collider2D.bounds.center; }

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    protected override void Initialize()
    {
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _skillSystem = gameObject.GetOrAddComponent<SkillSystem>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.freezeRotation = true;
        _rigidbody2D.linearVelocity = Vector3.zero;
        _rigidbody2D.mass = 2000;
        GameManager.Instance.OnMoveDirChanged += (dir) => { _moveDir = dir; };
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
        float speed = GameManager.Instance.PlayerInfo.Speed;
        Vector3 moveDir = transform.position + (Vector3)_moveDir * speed * Time.deltaTime;
        _rigidbody2D.MovePosition(moveDir);
        _spriteRenderer.flipX = _moveDir.x < 0;
        _animator.SetBool(Define.isMoveHash, _moveDir != Vector2.zero);
    }
}
