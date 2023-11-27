using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float armor;
    [SerializeField] private float damage;

    public GameObject ModelPrefab { get => modelPrefab; }
    public float Speed { get => speed; }
    public float MaxHealth { get => maxHealth; }
    public float Armor { get => armor; }
    public float Damage { get => damage; }
}
