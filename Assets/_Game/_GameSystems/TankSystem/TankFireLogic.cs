using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFireLogic : MonoBehaviour
{
    private TankSystem _tankSystem;
    private ObjectsPool _objectsPool;
    private EnumObjectType _currentType = EnumObjectType.FirstWeapon;

    private bool _isCanFire = true;

    private void Awake()
    {
        _tankSystem = FindObjectOfType<TankSystem>();
        _objectsPool = FindObjectOfType<ObjectsPool>();
    }

    private void OnEnable()
    {
        _tankSystem.OnPrevWeapon += WeaponSwitcher;
        _tankSystem.OnNextWeapon += WeaponSwitcher;
        _tankSystem.OnFire += Fire;
    }

    private void OnDisable()
    {
        _tankSystem.OnPrevWeapon -= WeaponSwitcher;
        _tankSystem.OnNextWeapon -= WeaponSwitcher;
        _tankSystem.OnFire -= Fire;
    }

    private void WeaponSwitcher()
    {
        _currentType = _currentType == EnumObjectType.FirstWeapon ? EnumObjectType.SecondWeapon : EnumObjectType.FirstWeapon;
        StopAllCoroutines();
        _isCanFire = true;
    }

    private void Fire()
    {
        if (!_isCanFire) return;
        GameObject weaponGO = _objectsPool.GetObject(_currentType);
        if (weaponGO == null) Debug.LogError("Empty");
        weaponGO.SetActive(true);
        weaponGO.transform.position = _tankSystem.tank.BarrelPoint.position;
        Weapon weapon = weaponGO.GetComponent<Weapon>();
        weapon.Fire(_tankSystem.tank.BarrelPoint.forward);
        StartCoroutine(FireDelayCoroutine(weapon.WeaponSettings.SelfDeactivateDelay));
    }

    private IEnumerator FireDelayCoroutine(float delay)
    {
        _isCanFire = false;
        yield return new WaitForSeconds(delay);
        _isCanFire = true;
    }
}
