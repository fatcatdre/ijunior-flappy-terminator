using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] protected InputSource _input;

    protected Animator _animator;
    protected int _isShooting = Animator.StringToHash("isShooting");
    protected int _isDead = Animator.StringToHash("isDead");
    protected bool _isCharacterDead;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void Respawn()
    {
        _isCharacterDead = false;

        _animator.SetBool(_isDead, false);
    }

    public virtual void Shoot(bool isShooting)
    {
        _animator.SetBool(_isShooting, isShooting);
    }

    public virtual void Die()
    {
        _isCharacterDead = true;

        _animator.SetBool(_isDead, true);
        _animator.SetBool(_isShooting, false);
    }
}
