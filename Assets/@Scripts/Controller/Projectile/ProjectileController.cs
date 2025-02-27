using UnityEngine;

public class ProjectileController : BaseController
{
    CircleCollider2D _circleCollider;
    Rigidbody2D _rididbody2D;
    Vector3 _moveDir;

    protected override void Initialize()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.radius = 0.1f;
        _circleCollider.isTrigger = true;
        _rididbody2D = GetComponent<Rigidbody2D>();
        _rididbody2D.bodyType = RigidbodyType2D.Kinematic;
        _moveDir = (GameManager.Instance.Target.transform.position -
            transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(_moveDir * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable == null || damageable.Tag != Define.EnemyTag)
            return;
        float atk = GameManager.Instance.PlayerInfo.Atk;
        GameObject causer = ObjectManager.Instance.Player.gameObject;
        damageable.AnyDamage(atk, causer);
        ObjectManager.Instance.Despawn(this);
    }
}
