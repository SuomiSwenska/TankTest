using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealthLogic : MonoBehaviour
{
    private TankSystem _tankSystem;
    private float _health;

    private void Awake()
    {
        _tankSystem = FindObjectOfType<TankSystem>();
    }

    private void Start()
    {
        _health = _tankSystem.tankSettings.TankHealth;
    }

    private void OnEnable()
    {
        _tankSystem.OnTankDamage += TankInjury;
    }

    private void OnDisable()
    {
        _tankSystem.OnTankDamage -= TankInjury;
    }

    private void TankInjury(float damage)
    {
        _health -= (damage * _tankSystem.tankSettings.TankArmor);
        if (_health <= 0) _tankSystem.OnTankDestroy?.Invoke();
    }
}
