using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public WeaponItem currentWeapon;

    public bool isLeftHandSlot;
    public bool isRightHandSlot;
    public bool isBackSlot;

    public GameObject currentWeaponModel;

    public void UnloadWeapon()
    {
        if(currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);
        }
    }
    
    public void UnloadWeaponAndDestroy()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponItem weaponItem)
    {
        UnloadWeaponAndDestroy();

        if (weaponItem == null)
        {
            UnloadWeapon();
            return;
        }


        GameObject model;

        if (weaponItem.m_goPrefab != null)
            model = weaponItem.m_goPrefab;
        else
            model = Managers.Resource.Instantiate(weaponItem.m_sPrefabPath);

        if(model != null)
        {
            if(parentOverride != null)
            {
                model.transform.parent = parentOverride;

            }
            else
            {
                model.transform.parent = transform;
            }

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currentWeaponModel = model;
    }
}
