using UnityEngine;
using UnityEngine.Pool;

public abstract class PooledObject : MonoBehaviour
{
    protected ObjectPool<PooledObject> _pool;

    private bool _isReturningToPool;

    public void SetPool(ObjectPool<PooledObject> pool)
    {
        _pool = pool;
    }

    public void OnTakenFromPool()
    {
        _isReturningToPool = false;
    }

    protected virtual void ReturnToPool()
    {
        if (_isReturningToPool)
            return;

        _isReturningToPool = true;

        if (_pool != null)
            _pool.Release(this);
        else
            Destroy(gameObject);
    }
}
