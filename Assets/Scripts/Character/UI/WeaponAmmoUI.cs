using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] TMP_Text weaponNameText;
    [SerializeField] TMP_Text weaponCurrentAmmoText;
    [SerializeField] TMP_Text weaponMaxAmmoText;

    WeaponComponent weaponComponent;

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEvent += OnWeaponEquipped;
    }
    private void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEvent -= OnWeaponEquipped;
    }
   

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent) return;

        weaponNameText.text = weaponComponent.WeaponInformation.WeaponName;
        weaponCurrentAmmoText.text = weaponComponent.WeaponInformation.BulletsInClip.ToString();
        weaponMaxAmmoText.text = weaponComponent.WeaponInformation.BulletAvailable.ToString();
    }
}
