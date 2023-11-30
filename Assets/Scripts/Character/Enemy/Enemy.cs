using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Enemy : Character, IDamageable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _scoreValue;
    [SerializeField] private float _gravityScaleWhenDead;
    [SerializeField] private Vector2 _forceWhenHit;
    [SerializeField] private float _delayBeforeDespawn;

    public UnityAction<Enemy> Died;

    private float _originalGravityScale;

    public int ScoreValue => _scoreValue;

    protected override void Awake()
    {
        base.Awake();

        _originalGravityScale = _rigidbody.gravityScale;
    }

    private void Update()
    {
        if (_isDead)
            return;

        Shoot();
        Move();
    }

    private void Move()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * Vector2.left);
    }

    public void SetBulletPool(Pool bulletPool)
    {
        _weapon.SetPool(bulletPool);
    }

    public override void Respawn()
    {
        base.Respawn();

        _rigidbody.gravityScale = _originalGravityScale;
        _rigidbody.velocity = Vector2.zero;
    }

    public override void Die()
    {
        if (_isDead)
            return;

        base.Die();

        _rigidbody.gravityScale = _gravityScaleWhenDead;
        _rigidbody.AddForce(_forceWhenHit);

        Died?.Invoke(this);

        StartCoroutine(Despawn());
    }

    public void TakeDamage()
    {
        Die();
    }

    private IEnumerator Despawn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _delayBeforeDespawn)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ReturnToPool();
    }
}
