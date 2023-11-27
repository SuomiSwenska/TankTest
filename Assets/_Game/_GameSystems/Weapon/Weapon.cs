using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponSettings _weaponSettings;
    private Rigidbody _weaponRigidbody;

    public Rigidbody WeaponRigidbody { get => _weaponRigidbody; }
    public WeaponSettings WeaponSettings { get => _weaponSettings; }

    private void Awake()
    {
        _weaponRigidbody = GetComponent<Rigidbody>();
    }

    public void Init(WeaponSettings weaponSettingsS)
    {
        _weaponSettings = weaponSettingsS;
    }

    public void Fire(Vector3 direction)
    {
        StopAllCoroutines();
        _weaponRigidbody.AddForce(direction * _weaponSettings.Force, ForceMode.Impulse);
        StartCoroutine(SelfDeactivatorCoroutine());
    }

    private IEnumerator SelfDeactivatorCoroutine()
    {
        yield return new WaitForSeconds(_weaponSettings.SelfDeactivateDelay);
        Deactivate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.transform.GetComponentInParent<Enemy>();
        enemy?.Injury(_weaponSettings.Damage);
        Deactivate();
    }

    private void Deactivate()
    {
        _weaponRigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
