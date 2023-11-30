using UnityEngine;

public class MouseInput : InputSource
{
    private bool _isJumping;
    private bool _isShooting;

    public override bool IsJumping => _isJumping;
    public override bool IsShooting => _isShooting;

    private void Update()
    {
        _isJumping = Input.GetMouseButtonDown(1);
        _isShooting = Input.GetMouseButton(0);
    }
}
