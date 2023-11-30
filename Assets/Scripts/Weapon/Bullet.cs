using UnityEngine;

public class Bullet : PooledObject
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage();

        ReturnToPool();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
