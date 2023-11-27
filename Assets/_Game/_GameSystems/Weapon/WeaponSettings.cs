using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSettings : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float damage;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float force;
    [SerializeField] private float selfDeactivateDelay;
    [SerializeField] private EnumObjectType objectType;

    public GameObject Prefab { get => prefab; }
    public float Damage { get => damage; }
    public float ReloadSpeed { get => reloadSpeed; }
    public EnumObjectType ObjectType { get => objectType; }
    public float Force { get => force; }
    public float SelfDeactivateDelay { get => selfDeactivateDelay; }
}
