using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyFabric _enemyFabric;

    [SerializeField]
    private List<Transform> _spawnPositions;

    [SerializeField]
    private List<EnemyAI> _enemyPool = new List<EnemyAI>();

    [SerializeField]
    private GameManager _gameManager;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void SpawnWave()
    {
        EnemyAI spawnedEnemy;

        foreach (var point in _spawnPositions)
        {
            Debug.Log("Position");

            spawnedEnemy = _enemyFabric.Get(DefaultEnemyTypes.melee, point.position);

            _enemyPool.Add(spawnedEnemy);

            spawnedEnemy.HealthHandler.OnDeath += _gameManager.CheckWinCondition;

            spawnedEnemy.Initialize(this);

            //_currentCountEnemies++;
        }
    }

    public GameObject GetPlayer()
    {
        return _gameManager.GetPlayer();
    }

    public void Recicle(EnemyAI enemy)
    {
        _enemyPool.Remove(enemy);

        Destroy(enemy.gameObject);

    }

    public int GetEnenmyCount()
    {
        return _enemyPool.Count;
    }
}
