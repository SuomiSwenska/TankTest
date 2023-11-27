using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private ObjectsPool _objectsPool;

    [SerializeField] private List<WeaponSettings> _weapons;
    [SerializeField] private int _weaponFirstPoolSize;
    [SerializeField] private int _weaponSecondPoolSize;

    [SerializeField] private List<GameObject> _enemiesPrefabs;
    [SerializeField] private int _enemyFirstPoolSize;
    [SerializeField] private int _enemySecondPoolSize;

    private void Awake()
    {
        _objectsPool = FindObjectOfType<ObjectsPool>();
    }

    private void Start()
    {
        FillObjectPools();
    }

    private void FillObjectPools()
    {
        for (int i = 0; i < _weaponFirstPoolSize; i++)
        {
            GameObject _weaponGO = Instantiate(_weapons[0].Prefab);
            _weaponGO.SetActive(false);
            _weaponGO.GetComponent<Weapon>().Init(_weapons[0]);
            _objectsPool.AddObject(EnumObjectType.FirstWeapon, _weaponGO);
        }

        for (int i = 0; i < _weaponSecondPoolSize; i++)
        {
            GameObject _weaponGO = Instantiate(_weapons[1].Prefab);
            _weaponGO.SetActive(false);
            _weaponGO.GetComponent<Weapon>().Init(_weapons[1]);
            _objectsPool.AddObject(EnumObjectType.SecondWeapon, _weaponGO);
        }

        for (int i = 0; i < _enemyFirstPoolSize; i++)
        {
            GameObject enemy = Instantiate(_enemiesPrefabs[0]);
            enemy.SetActive(false);
            _objectsPool.AddObject(EnumObjectType.EnemySmall, enemy);
        }

        for (int i = 0; i < _enemySecondPoolSize; i++)
        {
            GameObject enemy = Instantiate(_enemiesPrefabs[1]);
            enemy.SetActive(false);
            _objectsPool.AddObject(EnumObjectType.EnemyBig, enemy);
        }
    }
}
