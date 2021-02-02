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

    private PlayerController PlayerController;
    CrosshairScript PlayerCrosshair;
    private Animator Playeranimator;

    public Camera ViewCamera;

    private Transform GripIKLocation;

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
            WeaponComponent weapon = spawnWeapon.GetComponent<WeaponComponent>();
            if(weapon)
            {
                GripIKLocation = weapon.GripLocation;
            }
        }
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
        Debug.Log(IndependentMousePosition);
        Playeranimator.SetFloat("AimHorizontal", IndependentMousePosition.x);
        Playeranimator.SetFloat("AimVertical", IndependentMousePosition.y);
    }

    public void OnReloading(InputValue pressed)
    {
        Debug.Log("Reloading");
        Playeranimator.SetBool("IsReloading", pressed.isPressed);
    }

    public void OnFire(InputValue pressed)
    {
        Debug.Log("firing");
        Playeranimator.SetBool("IsFiring", pressed.isPressed);
    }
}
