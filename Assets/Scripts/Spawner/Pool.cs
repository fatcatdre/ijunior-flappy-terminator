using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private PooledObject _prefab;
    [SerializeField] private Transform _container;

    [Header("Pool Settings")]
    [SerializeField] private bool _collectionCheck;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<PooledObject> _pool;

    private void OnEnable()
    {
        _pool = new ObjectPool<PooledObject>(
            OnCreatePoolObject, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject,
            _collectionCheck, _defaultCapacity, _maxSize);
    }

    private void OnDisable()
    {
        foreach (Transform t in _container)
            Destroy(t.gameObject);
    }

    public PooledObject Get() => _pool.Get();

    private PooledObject OnCreatePoolObject()
    {
        var poolObject = Instantiate(_prefab, _container);
        poolObject.SetPool(_pool);

        return poolObject;
    }

    private void OnTakeFromPool(PooledObject poolObject)
    {
        poolObject.OnTakenFromPool();
        poolObject.gameObject.SetActive(true);
    }

    private void OnReturnToPool(PooledObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(PooledObject poolObject)
    {
        Destroy(poolObject.gameObject);
    }
}
