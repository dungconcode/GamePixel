using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Player_Index playerIndex; // Assuming you have a Player_Index scriptable object to manage player stats
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Armor")]
    public int maxArmor;
    public int currentArmor;
    private bool canRengerArmor = true;
    private Coroutine regenArmorCoroutine;

    [Header("Damage Popup")]
    [SerializeField] private GameObject damagePopupPrefab;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        maxHealth = (int)playerIndex.hp;
        currentHealth = maxHealth;
        maxArmor = (int)playerIndex.armor;
        currentArmor = maxArmor;
        StartCoroutine(RegenerateArmor());
    }
    IEnumerator RegenerateArmor()
    {
        maxHealth = (int)playerIndex.hp;
        maxArmor = (int)playerIndex.armor;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (canRengerArmor && currentArmor < maxArmor)
            {
                currentArmor += 1;
                if (currentArmor > maxArmor)
                {
                    currentArmor = maxArmor;
                }
            }
        }
    }
    public void TakeDamage(int _damage)
    {
        currentArmor -= _damage;
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Vector3 randomOffset = new Vector3(Random.Range(-0.6f, 0.5f), Random.Range(-0.1f, 0.3f), 0);
        GameObject popup = Instantiate(damagePopupPrefab, transform.position + offset + randomOffset, Quaternion.identity);
        Destroy(popup, 0.5f);
        if (currentArmor < 0)
        {
            currentHealth -= (_damage);
            FindObjectOfType<IndexBar>().OnDamage();
            currentArmor = 0;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
                return;
            }
        }
        if (regenArmorCoroutine != null)
        {
            StopCoroutine(regenArmorCoroutine);
        }
        regenArmorCoroutine = StartCoroutine(AfterTakeDamage());
        
    }
    IEnumerator AfterTakeDamage()
    {
        canRengerArmor = false;
        yield return new WaitForSeconds(2f);
        canRengerArmor = true;
    }
}
