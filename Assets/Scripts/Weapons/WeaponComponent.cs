using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    NONE,
    PISTOL,
    MACHINEGUN
}
[Serializable]
public struct WeaponStats
{
    public WeaponType WeaponType;
    public string WeaponName;
    public float Damage;
    public int BulletsInClip;
    public int ClipSize;
    public int BulletAvailable;
    public float FireStartDelay;
    public float FireRate;
    public float FireDistance;
    public bool Repeating;
    public LayerMask WeaponHitLayer;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform GripLocation => GripIKLocation;
    [SerializeField] private Transform GripIKLocation;

    public WeaponStats WeaponInformation => WeaponStats;
    [SerializeField] protected WeaponStats WeaponStats;

    [SerializeField] public Transform ParticleSpawnLocation;

    [SerializeField] protected GameObject FiringAnimation;

    protected WeaponHolder WeaponHolder;
    protected CrosshairScript crossHair;
    protected Camera MainCamera;
    protected ParticleSystem firingEffect;

    public bool Firing { get; private set; }
    public bool Reloading { get; private set; }
    private void Awake()
    {
        MainCamera = Camera.main;
    }
    public void Initialize(WeaponHolder weaponHolder, WeaponScriptable weaponScriptable)
    {
        WeaponHolder = weaponHolder;
        crossHair = weaponHolder.PlayerCrosshair;
        if(weaponScriptable)
        {
            WeaponStats = weaponScriptable.WeaponStats;
        }
    }

    public virtual void StartFiringWeapon()
    {
        Firing = true;
        if(WeaponStats.Repeating) // automatic gun
        {
            InvokeRepeating(nameof(FireWeapon), WeaponStats.FireStartDelay, WeaponStats.FireRate);
        }
        else // single shot gun
        {
            FireWeapon();
        }
    }
    public virtual void StopFiringWeapon()
    {
        Firing = false;
        if (firingEffect) Destroy(firingEffect.gameObject);
        CancelInvoke(nameof(FireWeapon));
    }
    protected virtual void FireWeapon()
    {
        //print("Firing weaponz pew pew");
        WeaponStats.BulletsInClip--;
    }

    public virtual void StartReloading()
    {
        Reloading = true;
        ReloadWeapon();
    }
    public virtual void StopReloading()
    {
        Reloading = false;
    }
    protected virtual void ReloadWeapon()
    {
        if (firingEffect) Destroy(firingEffect.gameObject);

        int bulletsToReload = WeaponStats.ClipSize - WeaponStats.BulletAvailable;
        if(bulletsToReload < 0)
        {
            WeaponStats.BulletsInClip = WeaponStats.ClipSize;
            WeaponStats.BulletAvailable -= WeaponStats.ClipSize;
        }
        else
        {
            WeaponStats.BulletsInClip = WeaponStats.BulletAvailable;
            WeaponStats.BulletAvailable = 0;
        }
    }
}
