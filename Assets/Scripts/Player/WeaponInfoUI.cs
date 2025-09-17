using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    public Image pannel;
    public static WeaponInfoUI instance;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text state;
    private void Awake()
    {
        instance = this;
        pannel.gameObject.SetActive(false);
    }
    public void ShowInfo(WeaponBase weapon)
    {
        if (weapon == null)
        {
            pannel.gameObject.SetActive(false);
            return;
        }
        weaponName.text = weapon.weaponName;
        state.text = weapon.weaponType.ToString();
        pannel.gameObject.SetActive(true);
    }
    public void HideInfo()
    {
        pannel.gameObject.SetActive(false);
    }
}
