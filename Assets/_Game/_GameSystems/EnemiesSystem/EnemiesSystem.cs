using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSystem : MonoBehaviour
{
    public Action<GameObject> OnEnemyDeath;
    public int enemiesInSsceneCount;
    public float delayBetweenSpawn;
    public List<Transform> spawnPointsTransforms;
}
