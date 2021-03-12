using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void OnWeaponEquippedEvent(WeaponComponent weaponComponent);

    public static event OnWeaponEquippedEvent OnWeaponEvent;

    public static void Invoke_OnWeaponEquippedEvent(WeaponComponent weaponComponent)
    {
        OnWeaponEvent?.Invoke(weaponComponent);
    }

    public delegate void OnHealthInitializeEvent(HealthComponant healthComponant);

    public static event OnHealthInitializeEvent OnHealthInitialize;

    public static void Invoke_OnHealthInitializeEvent(HealthComponant healthComponent)
    {
        OnHealthInitialize?.Invoke(healthComponent);
    }
}
