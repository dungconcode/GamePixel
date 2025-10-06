using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public enum EnemyType
{
    Normal,
    TrapCircleBullet,
    TrapToxicArea,
    TrapBoom
}
public class Enemy_Health : MonoBehaviour
{
   
    public Action onDeath;
    public float curentHealth;
    public float maxHealth;
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private GameObject enemyHealth;
    //[SerializeField] private TMP_Text damagePopUpTxT;

    public static event Action<Enemy_Health, Vector3, EnemyType> OnEnemyDeath;
    public EnemyType enemyType;

    private bool isDead = false;

    private void Start()
    {
        curentHealth = maxHealth;
        
    }
    public void TakeDamage(float damage)
    {
        if (gameObject.activeInHierarchy == false) return;
        StartCoroutine(DelayEnemyHeart(0.1f));
        curentHealth -= damage;
        ShowDamagePopup(damage);
        Debug.Log(isDead);
        Debug.Log(curentHealth);
        if (curentHealth <= 0 && !isDead)
        {
            //StartCoroutine(DieDelay(0.5f));
            Debug.Log("Enemy Die");
            Die();
        }
    }
    IEnumerator DelayEnemyHeart(float timeDuration)
    {
        GameObject heart = Instantiate(enemyHealth, transform.position, Quaternion.identity);
        heart.transform.localScale = transform.localScale;
        SpriteRenderer sr = heart.GetComponent<SpriteRenderer>();
        SpriteRenderer enemySR = GetComponent<SpriteRenderer>();
        sr.sprite = enemySR.sprite;
        Color color = sr.color;
        color.a = 0.5f;
        sr.color = color;
        heart.transform.SetParent(transform);
        yield return new WaitForSeconds(timeDuration);
        color.a = 1f;
        sr.color = color;
        heart.transform.SetParent(null);
        Destroy(heart);
    }
    private void ShowDamagePopup(float damage)
    {
        if (damagePopupPrefab != null)
        {
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-0.6f, 0.5f), UnityEngine.Random.Range(-0.1f, 0.3f), 0);
            GameObject popup = Instantiate(damagePopupPrefab, transform.position + offset + randomOffset, Quaternion.identity);
            TMP_Text tmp = popup.GetComponentInChildren<TMP_Text>();
            if (tmp != null)
            {
                tmp.text = damage.ToString();
            }
            Destroy(popup, 0.5f); // Destroy the popup after 1 second
        }
    }
    private void Die()
    {
        if (isDead) return;  // tránh gọi lại
        isDead = true;
        GetComponent<EnemyDropGold>().DropGold();
        OnEnemyDeath?.Invoke(this, transform.position, enemyType);
        onDeath?.Invoke();
        onDeath = null;
        gameObject.SetActive(false);
        
        Destroy(gameObject,1f);
    }

}
