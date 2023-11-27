using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponsFirst;
    [SerializeField] private List<GameObject> _weaponsSecond;
    [SerializeField] private List<GameObject> _enemySmall;
    [SerializeField] private List<GameObject> _enemyBig;

    private void Awake()
    {
        _weaponsFirst = new List<GameObject>();
        _weaponsSecond = new List<GameObject>();
        _enemySmall = new List<GameObject>();
        _enemyBig = new List<GameObject>();
    }

    public void AddObject(EnumObjectType type, GameObject createdGO)
    {
        switch (type)
        {
            case EnumObjectType.None:
                break;
            case EnumObjectType.FirstWeapon:
                _weaponsFirst.Add(createdGO);
                break;
            case EnumObjectType.SecondWeapon:
                _weaponsSecond.Add(createdGO);
                break;
            case EnumObjectType.EnemySmall:
                _enemySmall.Add(createdGO);
                break;
            case EnumObjectType.EnemyBig:
                _enemyBig.Add(createdGO);
                break;
            default:
                break;
        }
    }

    public GameObject GetObject(EnumObjectType type)
    {
        switch (type)
        {
            case EnumObjectType.None:
                return null;
            case EnumObjectType.FirstWeapon:
                foreach (var item in _weaponsFirst)
                {
                    if (!item.activeSelf) return item;
                }
                return null;
            case EnumObjectType.SecondWeapon:
                foreach (var item in _weaponsSecond)
                {
                    if (!item.activeSelf) return item;
                }
                return null;
            case EnumObjectType.EnemySmall:
                foreach (var item in _enemySmall)
                {
                    if (!item.activeSelf) return item;
                }
                return null;
            case EnumObjectType.EnemyBig:
                foreach (var item in _enemyBig)
                {
                    if (!item.activeSelf) return item;
                }
                return null;
            default:
                return null;
        }
    }
}
