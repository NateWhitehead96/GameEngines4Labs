using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{

    private Vector3 hitLocation;
    protected override void FireWeapon()
    {
        base.FireWeapon();
        print("Firing weaponz pew pew");
        if(WeaponStats.BulletsInClip > 0 && !Reloading && !WeaponHolder.PlayerController.IsRunning)
        {

        Ray screenRay = MainCamera.ScreenPointToRay(new Vector3(crossHair.CurrentAimPos.x, crossHair.CurrentAimPos.y, 0));

        if (!Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.FireDistance, WeaponStats.WeaponHitLayer))
        {
            return;
        }
        hitLocation = hit.point;
        Vector3 hitDirection = hit.point - MainCamera.transform.position;
        Debug.DrawRay(MainCamera.transform.position, hitDirection.normalized * WeaponStats.FireDistance, Color.red);
        }
        else if(WeaponStats.BulletsInClip <= 0)
        {
            if (!WeaponHolder) return;
            WeaponHolder.startreloading();
        }
        

    }
    private void OnDrawGizmos()
    {
        if(hitLocation != Vector3.zero)
        {
            Gizmos.DrawWireSphere(hitLocation, 0.2f);
        }
    }

}
