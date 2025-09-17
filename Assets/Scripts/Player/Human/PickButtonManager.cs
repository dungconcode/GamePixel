using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickButtonManager : MonoBehaviour
{
    public static PickButtonManager instance;
    public Button pickUpButton;
    public WeaponManager player;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>();
        pickUpButton.gameObject.SetActive(false);

        // Gán sự kiện button -> gọi hàm player nhặt
        pickUpButton.onClick.AddListener(() => player.PickUpWeapon());
    }

    public void ShowPickUpButton(bool show)
    {
        pickUpButton.gameObject.SetActive(show);
    }
}
