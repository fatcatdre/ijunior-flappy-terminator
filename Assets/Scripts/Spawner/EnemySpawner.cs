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

    public UnityAction<int> EnemyDied;

    private List<SpawnPoint> _spawnPoints;
    private Queue<SpawnPoint> _recentlyUsedSpawnPoints;

    private float _elapsedTime;

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());
        _recentlyUsedSpawnPoints = new Queue<SpawnPoint>(_spawnsBeforeReuse);
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            while (_elapsedTime < _timeBetweenSpawns)
            {
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            PrepareEnemy(_enemyPool.Get() as Enemy);
            RefreshSpawnPoints();

            _elapsedTime = 0f;
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

    private void OnEnemyDied(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy.ScoreValue);
        enemy.Died -= OnEnemyDied;
    }
}
