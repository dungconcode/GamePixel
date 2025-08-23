using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase[] weaponSlot = new WeaponBase[2];
    public int currentWeaponSlot;
    public WeaponBase currentWeapon;
    public GameObject weaponPosition;
    public Transform fireWeapon;

    private void Start()
    {
        if (weaponSlot[1] != null)
            weaponSlot[1].gameObject.SetActive(false);

        if (weaponSlot[0] != null)
            EpicWeapon(0); 
    }
    public void EpicWeapon(int weaponIndex)
    {
        currentWeapon = weaponSlot[weaponIndex];
        currentWeaponSlot = weaponIndex;
        for(int i = 0; i < weaponSlot.Length; i++)
        {
            if (weaponSlot[i] != null)
            {
                weaponSlot[i].gameObject.SetActive(false);
            }
        }
        if (currentWeapon != null)
        {
            Debug.Log("Đang cầm: " + currentWeapon.weaponName);
            currentWeapon.gameObject.SetActive(true);
        }  
        else
            Debug.Log("Slot trống");
    }
    public void ChangeWeapon()
    {
        int nextWeaponSlot = (currentWeaponSlot + 1) % weaponSlot.Length;  
        EpicWeapon(nextWeaponSlot);
    }
    public void PickWeapon(GameObject newWeapon)
    {
        for(int i = 0; i < weaponSlot.Length; i++)
        {
            if (weaponSlot[i] == null)
            {
                WeaponBase weaponType = newWeapon.GetComponent<WeaponBase>();
                if (weaponType.weaponType == WeaponType.Melee || weaponType.weaponType == WeaponType.Chain)
                {
                    GameObject weaponObj = Instantiate(newWeapon, weaponPosition.transform);
                    weaponObj.transform.localPosition = Vector3.zero;
                    weaponObj.transform.localRotation = Quaternion.identity;
                    weaponSlot[i] = weaponObj.GetComponent<WeaponBase>();
                }
                if(weaponType.weaponType == WeaponType.Magic)
                {
                    GameObject weaponObj = Instantiate(newWeapon, fireWeapon);
                    weaponObj.transform.localPosition = Vector3.zero;
                    weaponObj.transform.localRotation = Quaternion.identity;
                    weaponSlot[i] = weaponObj.GetComponent<WeaponBase>();
                }
                EpicWeapon(i);
                return;
            }

        }
    }
}
