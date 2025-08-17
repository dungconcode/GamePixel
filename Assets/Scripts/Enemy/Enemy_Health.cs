using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Health : MonoBehaviour
{
    public float curentHealth;
    public float maxHealth;
    [SerializeField] private GameObject damagePopupPrefab;
    //[SerializeField] private TMP_Text damagePopUpTxT;
    private void Start()
    {
        curentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        curentHealth -= damage;
        //ShowDamagePopup(damage);
        if(damagePopupPrefab != null)
        {
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 randomOffset = new Vector3(Random.Range(-0.6f, 0.5f), Random.Range(-0.1f, 0.3f), 0);
            GameObject popup = Instantiate(damagePopupPrefab, transform.position + offset + randomOffset, Quaternion.identity);
            TMP_Text tmp = popup.GetComponentInChildren<TMP_Text>();
            if (tmp != null)
            {
                tmp.text = damage.ToString();
            }
            Destroy(popup, 0.5f); // Destroy the popup after 1 second
        }
        if (curentHealth <= 0)
        {
            Die();
        }
    }
    private void ShowDamagePopup(float damage)
    {
        Vector3 offset = new Vector3(0, 1.5f, 0);
        GameObject popup = Instantiate(damagePopupPrefab, transform.position + offset, Quaternion.identity);
        Debug.Log("Damage popup instantiated at position: " + transform.position);
        popup.GetComponentInChildren<DamagePopUp>().Setup(damage);
    }
    private void Die()
    {
        EnemySpawn.Instance.EnemyDied(gameObject); // Notify the EnemySpawn to remove this enemy
        gameObject.SetActive(false); // Deactivate the enemy object
    }
}
