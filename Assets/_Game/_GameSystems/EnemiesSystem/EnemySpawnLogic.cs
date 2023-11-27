using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLogic : MonoBehaviour
{
    private EnemiesSystem _enemiesSystem;
    private ObjectsPool _objectsPool;
    private TankSystem _tankSystem;

    private void Awake()
    {
        _enemiesSystem = FindObjectOfType<EnemiesSystem>();
        _objectsPool = FindObjectOfType<ObjectsPool>();
        _tankSystem = FindObjectOfType<TankSystem>();
    }

    private void OnEnable()
    {
        _enemiesSystem.OnEnemyDeath += EnemyDeathHandler;
    }

    private void OnDisable()
    {
        _enemiesSystem.OnEnemyDeath -= EnemyDeathHandler;
    }

    private void Start()
    {
        StartCoroutine(InitSpawn());
    }

    private void EnemyDeathHandler(GameObject enemyGO)
    {
        Debug.Log("Enemy " + enemyGO.name + " is dead");
        enemyGO.SetActive(false);
        SpawnEnemy();
    }

    private IEnumerator InitSpawn()
    {
        for (int i = 0; i < _enemiesSystem.enemiesInSsceneCount; i++)
        {
            yield return new WaitForSeconds(_enemiesSystem.delayBetweenSpawn);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int random = Random.Range(0, 100);
        GameObject enemy = _objectsPool.GetObject(random < 50 ? EnumObjectType.EnemySmall : EnumObjectType.EnemyBig);
        enemy.SetActive(true);
        enemy.transform.position = _enemiesSystem.spawnPointsTransforms[Random.Range(0, _enemiesSystem.spawnPointsTransforms.Count)].position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.GetComponent<Enemy>().StartPursue(_tankSystem.tank.transform);
    }
}
