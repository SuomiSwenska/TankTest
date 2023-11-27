using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _barrelPoint;
    [SerializeField] private Rigidbody _rigidbody;

    public Transform Turret { get => _turret; }
    public Transform BarrelPoint { get => _barrelPoint; }
    public Rigidbody Rigidbody { get => _rigidbody; private set => _rigidbody = value; }
    public Transform Body { get => _body; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
