using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Pool _enemyPool;
    [SerializeField] private Pool _bulletPool;
    [SerializeField] private Transform _container;
    [SerializeField] private int _spawnsBeforeReuse;
    [SerializeField] private float _timeBetweenSpawns;

    private List<SpawnPoint> _spawnPoints;
    private Queue<SpawnPoint> _recentlyUsedSpawnPoints;
    private WaitForSeconds _spawnDelay;

    public event UnityAction<int> EnemyDied;

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());
        _recentlyUsedSpawnPoints = new Queue<SpawnPoint>(_spawnsBeforeReuse);
    }

    private void OnEnable()
    {
        UpdateSpawnDelay();
        StartCoroutine(Spawn());
    }

    private void OnValidate()
    {
        UpdateSpawnDelay();
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            yield return _spawnDelay;

            PrepareEnemy(_enemyPool.Get() as Enemy);
            RefreshSpawnPoints();
        }
    }

    private void PrepareEnemy(Enemy enemy)
    {
        SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        RemoveSpawnPoint(spawnPoint);

        enemy.Respawn();
        enemy.SetBulletPool(_bulletPool);
        enemy.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);
        enemy.Died += OnEnemyDied;
    }

    private void RefreshSpawnPoints()
    {
        if (_recentlyUsedSpawnPoints.Count < _spawnsBeforeReuse)
            return;

        SpawnPoint spawnPoint = _recentlyUsedSpawnPoints.Dequeue();
        _spawnPoints.Add(spawnPoint);
    }

    private void RemoveSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Remove(spawnPoint);
        _recentlyUsedSpawnPoints.Enqueue(spawnPoint);
    }

    private void UpdateSpawnDelay()
    {
        _spawnDelay = new WaitForSeconds(_timeBetweenSpawns);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy.ScoreValue);
        enemy.Died -= OnEnemyDied;
    }
}
