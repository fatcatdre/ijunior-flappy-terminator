using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimator : CharacterAnimator
{
    [SerializeField] private Transform _hand;
    [SerializeField] private float _minAngleZ;
    [SerializeField] private float _minAngleVelocity;
    [SerializeField] private float _maxAngleZ;
    [SerializeField] private float _maxAngleVelocity;

    private Rigidbody2D _rigidbody;

    private Quaternion _minRotation;
    private Quaternion _maxRotation;

    private int _jumped = Animator.StringToHash("jumped");

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();

        _minRotation = Quaternion.Euler(0f, 0f, _minAngleZ);
        _maxRotation = Quaternion.Euler(0f, 0f, _maxAngleZ);
    }

    private void Update()
    {
        if (_isCharacterDead)
            return;

        UpdateAnimations();
        UpdateRotation();
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(_isShooting, _input.IsShooting);

        if (_input.IsJumping)
            _animator.SetTrigger(_jumped);
    }

    private void UpdateRotation()
    {
        float lerp = Mathf.InverseLerp(_minAngleVelocity, _maxAngleVelocity, _rigidbody.velocity.y);
        _hand.rotation = Quaternion.Lerp(_minRotation, _maxRotation, lerp);
    }
}
