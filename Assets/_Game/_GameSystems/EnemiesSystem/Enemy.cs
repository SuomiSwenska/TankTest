using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySettings _enemySettings;
    private Rigidbody _enemyRigidbody;
    private NavMeshAgent _navMeshAgent;
    private EnemiesSystem _enemiesSystem;
    private TankSystem _tankSystem;
    private Transform _target;
    private float _health;
    private bool _isAlive = true;

    public Rigidbody EnemyRigidbody { get => _enemyRigidbody; private set => _enemyRigidbody = value; }
    public EnemySettings EnemySettings { get => _enemySettings; }
    public float Health { get => _health; private set => _health = value; }

    private void Awake()
    {
        EnemyRigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemiesSystem = FindObjectOfType<EnemiesSystem>();
        _tankSystem = FindObjectOfType<TankSystem>();
    }

    public void StartPursue(Transform target)
    {
        Init();
        _target = target;
        StartCoroutine(RouteControllerCoroutine());
    }

    public void Injury(float damage)
    {
        Health -= (damage * _enemySettings.Armor);
        if (Health <= 0) Death();
    }

    private void Init()
    {
        _isAlive = true;
        Health = _enemySettings.MaxHealth;
        EnemyRigidbody.velocity = Vector3.zero;
        _navMeshAgent.speed = _enemySettings.Speed;
    }

    private IEnumerator RouteControllerCoroutine()
    {
        while (_isAlive)
        {
            _navMeshAgent.SetDestination(_target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Death()
    {
        _isAlive = false;
        StopAllCoroutines();
        EnemyRigidbody.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;
        _enemiesSystem.OnEnemyDeath?.Invoke(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponentInParent<Tank>()) _tankSystem.OnTankDamage?.Invoke(_enemySettings.Damage);
    }
}
