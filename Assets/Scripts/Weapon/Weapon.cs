using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Pool _bulletPool;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _delayBetweenShots = 0.5f;

    private float _delayRemaining;

    private void Update()
    {
        _delayRemaining = Mathf.Max(0f, _delayRemaining - Time.deltaTime);
    }

    public void Shoot()
    {
        if (Mathf.Approximately(_delayRemaining, 0f) == false)
            return;

        var bullet = _bulletPool.Get();
        bullet.transform.SetPositionAndRotation(_muzzle.position, _muzzle.rotation);

        (bullet as Bullet)?.SetSpeed(_bulletSpeed);
        
        _delayRemaining = _delayBetweenShots;
    }

    public void SetPool(Pool pool)
    {
        _bulletPool = pool;
    }
}
