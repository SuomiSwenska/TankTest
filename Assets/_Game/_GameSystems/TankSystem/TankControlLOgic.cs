using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControlLOgic : MonoBehaviour
{
    private TankSystem _tankSystem;

    private bool _isForaward;
    private bool _isBackward;
    private bool _isLeft;
    private bool _isRight;

    private Plane groundPlane;

    private bool _isHoldedMoving;

    private void Awake()
    {
        _tankSystem = FindObjectOfType<TankSystem>();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void OnEnable()
    {
        _tankSystem.OnTankDestroy += DeathHandler;
    }

    private void OnDisable()
    {
        _tankSystem.OnTankDestroy -= DeathHandler;
    }

    private void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()
    {
        if (_isHoldedMoving) return;
        Move();
        TurretRotation();
    }

    private void InputHandler()
    {
        _isForaward = Input.GetKey(KeyCode.W);
        _isBackward = Input.GetKey(KeyCode.S);
        _isLeft = Input.GetKey(KeyCode.A);
        _isRight = Input.GetKey(KeyCode.D);

        if (Input.GetMouseButtonDown(0)) _tankSystem.OnFire?.Invoke();
        if (Input.GetKeyDown(KeyCode.Z)) _tankSystem.OnPrevWeapon?.Invoke();
        if (Input.GetKeyDown(KeyCode.X)) _tankSystem.OnNextWeapon?.Invoke();
    }

    private void Move()
    {
        Vector3 direction = (_isForaward ? _tankSystem.tank.transform.forward : Vector3.zero) + (_isBackward ? -_tankSystem.tank.transform.forward : Vector3.zero);

        if (direction == Vector3.zero) return;

        _tankSystem.tank.Rigidbody.MovePosition(Vector3.Lerp(_tankSystem.tank.transform.position, _tankSystem.tank.transform.position + 
            (direction * _tankSystem.tankSettings.MoveSpeed), Time.deltaTime));

        direction += (_isLeft ? -_tankSystem.tank.transform.right : Vector3.zero) + (_isRight ? _tankSystem.tank.transform.right : Vector3.zero);

        if (_isBackward) direction -= -_tankSystem.tank.transform.forward;

        _tankSystem.tank.Rigidbody.MoveRotation(Quaternion.Lerp(_tankSystem.tank.transform.rotation, 
            Quaternion.LookRotation(direction, _tankSystem.tank.transform.up), Time.deltaTime * _tankSystem.tankSettings.RotationSpeed));
    }


    private void TurretRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 hitPoint = Vector3.zero;

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            hitPoint = ray.GetPoint(rayDistance);
        }

        Vector3 turretToMouse = hitPoint - _tankSystem.tank.Turret.position;
        turretToMouse.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(turretToMouse);

        _tankSystem.tank.Turret.rotation = Quaternion.Lerp(_tankSystem.tank.Turret.rotation, 
            Quaternion.Euler(0, targetRotation.eulerAngles.y, 0), Time.deltaTime * _tankSystem.tankSettings.TurretRotationSpeed);
    }

    private void DeathHandler()
    {
        _isHoldedMoving = true;
        ChangeBodyColor();
    }

    private void ChangeBodyColor()
    {
        MeshRenderer tankBodyMehRenderer = _tankSystem.tank.GetComponent<Tank>().Body.GetComponent<MeshRenderer>();
        Material blackMaterial = Instantiate(tankBodyMehRenderer.sharedMaterial);
        blackMaterial.color = Color.black;
        tankBodyMehRenderer.sharedMaterial = blackMaterial;
    }
}

