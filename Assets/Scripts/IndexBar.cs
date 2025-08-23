using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndexBar : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private TextMeshProUGUI health_txt;
    [SerializeField] private Image health_bar;
    [SerializeField]private Image healthDamage;
    private Color healthDamageColor;
    private float damageHealthFadeTime;

    [Header("Armor")]
    [SerializeField] private TextMeshProUGUI armor_txt;
    [SerializeField] private Image armor_bar;

    private PlayerHealth playerHealth;
    private void Awake()
    {
        
        healthDamageColor = healthDamage.color;
        healthDamageColor.a = 0f;
        healthDamage.color = healthDamageColor;

    }
    private void Start()
    {
        playerHealth = PlayerHealth.instance;
        health_txt.text = playerHealth.currentHealth.ToString() + "/" + playerHealth.maxHealth.ToString();
        health_bar.fillAmount = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        armor_txt.text = playerHealth.currentArmor.ToString() + "/" + playerHealth.maxArmor.ToString();
        armor_bar.fillAmount = (float)playerHealth.currentArmor / playerHealth.maxArmor;
    }
    private void Update()
    {
        if (healthDamageColor.a > 0f)
        {
            damageHealthFadeTime -= Time.deltaTime;
            if (damageHealthFadeTime <= 0f)
            {
                float fadeAmout = 10f;
                healthDamageColor.a -= fadeAmout * Time.deltaTime;
                healthDamage.color = healthDamageColor;
            }
        }
        if (playerHealth != null)
        {
            
            health_txt.text = playerHealth.currentHealth.ToString() + "/" + playerHealth.maxHealth.ToString();
            health_bar.fillAmount = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            //Debug.Log("Current Health: " + playerHealth.maxHealth);
            armor_txt.text = playerHealth.currentArmor.ToString() + "/" + playerHealth.maxArmor.ToString();
            armor_bar.fillAmount = (float)playerHealth.currentArmor / playerHealth.maxArmor;
        }
    }
    public void OnDamage()
    {
        if(healthDamageColor.a <= 0f)
        {
            healthDamage.fillAmount = health_bar.fillAmount;
        }
        healthDamageColor.a = 0.5f;
        healthDamage.color = healthDamageColor;
        damageHealthFadeTime = 0.3f;
    }
}
