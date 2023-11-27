using System;
using UnityEngine;

public class TankSystem : MonoBehaviour
{
    public Action<float> OnTankDamage;
    public Action OnTankDestroy;
    public Action OnFire;
    public Action OnPrevWeapon;
    public Action OnNextWeapon;

    public Tank tank;
    public TankSettings tankSettings;
}
