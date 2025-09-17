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
    private bool isWeaponNearly;
    public WeaponPickUp weaponNearby;

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
        WeaponBase weaponType = newWeapon.GetComponent<WeaponBase>();
        for (int i = 0; i < weaponSlot.Length; i++)
        {
            if (weaponSlot[i] == null)
            {
                
                if (weaponType.weaponType == WeaponType.Melee || weaponType.weaponType == WeaponType.Chain)
                {
                    GameObject weaponObj = Instantiate(newWeapon, weaponPosition.transform);
                    WeaponPickUp tmp = weaponObj.GetComponent<WeaponPickUp>();
                    tmp.isPicked = true;
                    tmp.DestroyArrow();
                    //tmp.enabled = false;
                    weaponObj.transform.localPosition = Vector3.zero;
                    weaponObj.transform.localRotation = Quaternion.identity;
                    weaponSlot[i] = weaponObj.GetComponent<WeaponBase>();
                    
                }
                if(weaponType.weaponType == WeaponType.Magic)
                {
                    GameObject weaponObj = Instantiate(newWeapon, fireWeapon);
                    WeaponPickUp tmp = weaponObj.GetComponent<WeaponPickUp>();
                    tmp.isPicked = true;
                    tmp.DestroyArrow();
                    //tmp.enabled = false;
                    weaponObj.transform.localPosition = Vector3.zero;
                    weaponObj.transform.localRotation = Quaternion.identity;
                    weaponSlot[i] = weaponObj.GetComponent<WeaponBase>();
                }
                EpicWeapon(i);
                return;
            }
        }
        DropWeapon(currentWeaponSlot);
        if (weaponType.weaponType == WeaponType.Melee || weaponType.weaponType == WeaponType.Chain)
        {
            GameObject weaponObj = Instantiate(newWeapon, weaponPosition.transform);
            weaponObj.transform.localPosition = Vector3.zero;
            weaponObj.transform.localRotation = Quaternion.identity;
            weaponSlot[currentWeaponSlot] = weaponObj.GetComponent<WeaponBase>();
        }
        if (weaponType.weaponType == WeaponType.Magic)
        {
            GameObject weaponObj = Instantiate(newWeapon, fireWeapon);
            weaponObj.transform.localPosition = Vector3.zero;
            weaponObj.transform.localRotation = Quaternion.identity;
            weaponSlot[currentWeaponSlot] = weaponObj.GetComponent<WeaponBase>();
        }
        EpicWeapon(currentWeaponSlot);
    }
    private void DropWeapon(int weaponSlot)
    {
        if(this.weaponSlot[weaponSlot] != null)
        {
            GameObject weaponObj = Instantiate(this.weaponSlot[weaponSlot].gameObject, transform.position, Quaternion.identity);
            WeaponPickUp tmp = weaponObj.GetComponent<WeaponPickUp>();
            //tmp.enabled = true;
            tmp.isPicked = false;
            Destroy(this.weaponSlot[weaponSlot].gameObject);
            this.weaponSlot[weaponSlot] = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            weaponNearby = collision.GetComponent<WeaponPickUp>();
            PickButtonManager.instance.ShowPickUpButton(true);  // hiện nút
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && weaponNearby != null && collision.GetComponent<WeaponPickUp>() == weaponNearby)
        {
            weaponNearby = null;
            PickButtonManager.instance.ShowPickUpButton(false); // ẩn nút
        }
    }
    public void PickUpWeapon()
    {
        if (weaponNearby != null)
        {
            PickWeapon(weaponNearby.weaponData); // dùng code bạn đã có để gắn vào slot
            Destroy(weaponNearby.gameObject);
            weaponNearby = null;
            PickButtonManager.instance.ShowPickUpButton(false);
        }
    }
}
