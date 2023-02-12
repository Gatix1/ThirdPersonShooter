using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoader : MonoBehaviour
{
    public GameObject currentWeaponModel;
    public int ammo = 100;

    private void UnloadAndDestroyWeapon()
    {
        if (currentWeaponModel != null)
        {
            ammo = currentWeaponModel.GetComponent<GunController>().totalAmmo;
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(GunItem weapon)
    {
        UnloadAndDestroyWeapon();

        if(weapon == null)
        {
            return;
        }

            GameObject weaponModel = Instantiate(weapon.itemModel, transform);
            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;
            currentWeaponModel = weaponModel;
    }
}
