using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : PooledObject
{
    [SerializeField] protected InputSource _input;
    [SerializeField] protected CharacterAnimator _animator;

    protected Weapon _weapon;
    protected Rigidbody2D _rigidbody;
    protected bool _isDead;

    protected virtual void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void Respawn()
    {
        _isDead = false;
        _animator.Respawn();
    }

    public virtual void Die()
    {
        _isDead = true;
        _animator.Die();
    }

    protected virtual void Shoot()
    {
        if (_input.IsShooting)
            _weapon.Shoot();

        _animator.Shoot(_input.IsShooting);
    }
}
