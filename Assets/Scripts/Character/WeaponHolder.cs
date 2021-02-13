using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("Weapon to Spawn"), SerializeField]
    private GameObject WeaponToSpawn;

    [SerializeField]
    private Transform WeaponSocketLocation;

    public PlayerController PlayerController;
    CrosshairScript PlayerCrosshair;
    private Animator Playeranimator;

    public Camera ViewCamera;

    private Transform GripIKLocation;

    private WeaponComponent EquipedWeapon;

    private bool WasFiring = false;
    private bool FiringPressed = false;

    private void Awake()
    {
        Playeranimator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
        if (PlayerController)
        {
            PlayerCrosshair = PlayerController.Crosshair;
        }
        ViewCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnWeapon = Instantiate(WeaponToSpawn, WeaponSocketLocation.position, WeaponSocketLocation.rotation);
        if(spawnWeapon)
        {
            spawnWeapon.transform.parent = WeaponSocketLocation;
            EquipedWeapon = spawnWeapon.GetComponent<WeaponComponent>();
            EquipedWeapon.Initialize(this, PlayerCrosshair);
            if(EquipedWeapon)
            {
                GripIKLocation = EquipedWeapon.GripLocation;
                Playeranimator.SetInteger("WeaponType", (int)EquipedWeapon.WeaponInformation.WeaponType);
            }
        }
        PlayerEvents.Invoke_OnWeaponEquippedEvent(EquipedWeapon);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Playeranimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        Playeranimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLook(InputValue delta) // worked for me
    {
        Vector3 IndependentMousePosition = ViewCamera.ScreenToViewportPoint(PlayerCrosshair.CurrentAimPos);
        //Debug.Log(IndependentMousePosition);
        Playeranimator.SetFloat("AimHorizontal", IndependentMousePosition.x);
        Playeranimator.SetFloat("AimVertical", IndependentMousePosition.y);
    }

    public void OnReloading(InputValue pressed)
    {
        Debug.Log("Reloading");
        //Playeranimator.SetBool("IsReloading", pressed.isPressed);

        startreloading();
    }

    public void OnFire(InputValue pressed)
    {
        Debug.Log("firing");
        FiringPressed = pressed.isPressed;
        if(FiringPressed)
        {
            startfiring();
        }
        else
        {
            stopfiring();
        }
    }

    public void startfiring()
    {
        if (EquipedWeapon.WeaponInformation.BulletAvailable <= 0 && EquipedWeapon.WeaponInformation.BulletsInClip <= 0) return;
        PlayerController.IsFiring = true;
        Playeranimator.SetBool("IsFiring", true);
        EquipedWeapon.StartFiringWeapon();
    }

    public void stopfiring()
    {
        PlayerController.IsFiring = false;
        Playeranimator.SetBool("IsFiring", false);
        EquipedWeapon.StopFiringWeapon();
    }

    public void startreloading()
    {
        if(PlayerController.IsFiring)
        {
            WasFiring = true;
            stopfiring();
        }
        PlayerController.IsReloading = true;
        Playeranimator.SetBool("IsReloading", true);
        EquipedWeapon.StartReloading();
        InvokeRepeating(nameof(stopReloading), 0, 0.1f);
    }
    public void stopReloading()
    {
        if (Playeranimator.GetBool("IsReloading")) return;
        PlayerController.IsReloading = false;
        //Playeranimator.SetBool("IsReloading", false);
        EquipedWeapon.StopReloading();
        CancelInvoke(nameof(stopReloading));

        if(WasFiring && FiringPressed)
        {
            startfiring();
            WasFiring = false;
        }
    }
}
