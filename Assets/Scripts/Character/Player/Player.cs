using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Player : Character, IDamageable
{
    [SerializeField] private float _tapForce = 250f;

    private Vector2 _startingPosition;
    private Quaternion _startingRotation;
    private bool _isRespawning;

    public event UnityAction Died;

    protected override void Awake()
    {
        base.Awake();

        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
    }

    private void Start()
    {
        Respawn();
    }

    private void Update()
    {
        if (_isDead || _isRespawning)
            return;

        Jump();
        Shoot();
    }

    public override void Respawn()
    {
        base.Respawn();

        _isRespawning = true;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.rotation = 0f;
        _rigidbody.angularVelocity = 0f;

        transform.SetPositionAndRotation(_startingPosition, _startingRotation);

        StartCoroutine(RemoveRespawnDebuff());
    }

    public override void Die()
    {
        base.Die();

        Died?.Invoke();
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Jump()
    {
        if (_input.IsJumping)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
        }
    }

    private IEnumerator RemoveRespawnDebuff()
    {
        yield return null;

        _isRespawning = false;
    }
}
