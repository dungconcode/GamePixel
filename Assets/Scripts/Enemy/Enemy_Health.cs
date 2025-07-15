using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float curentHealth;
    public float maxHealth;
    private void Start()
    {
        curentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        curentHealth -= damage;
        if (curentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false); // Deactivate the enemy object
    }
}
