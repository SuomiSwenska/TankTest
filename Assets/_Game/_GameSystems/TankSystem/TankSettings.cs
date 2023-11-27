using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TankSettings : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float turretRotationSpeed;
    [SerializeField] private float tankHealth;
    [SerializeField] private float tankArmor;

    public float MoveSpeed { get => moveSpeed; }
    public float TurretRotationSpeed { get => turretRotationSpeed; }
    public float RotationSpeed { get => rotationSpeed; }
    public float TankHealth { get => tankHealth; }
    public float TankArmor { get => tankArmor; }
}
