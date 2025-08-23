using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject weaponData;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            Debug.Log("Picked up weapon: ");
            WeaponManager weaponManager = player.gameObject.GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.PickWeapon(weaponData);
                Destroy(gameObject);
            }
        }
    }
}
