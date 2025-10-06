using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Player_Index playerIndex;

    [Header("Reference")]
    public UpdateController update;
    //public CharacterStarts characterStarts;

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
    [SerializeField] private GameObject playerHeart;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        update = GetComponent<UpdateController>();
        LoadIndex();
        StartCoroutine(RegenerateArmor());
    }
    public void LoadIndex()
    {
        var runtimeData = update.runtimeData;
        maxHealth = (int)runtimeData.hp;
        currentHealth = maxHealth;
        maxArmor = (int)runtimeData.armor;
        currentArmor = maxArmor;
    }
    IEnumerator RegenerateArmor()
    {
        var runtimeData = update.runtimeData;
        maxHealth = (int)runtimeData.hp;
        maxArmor = (int)runtimeData.armor;
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
        if (gameObject.activeInHierarchy == false) return;
        StartCoroutine(DelayPLayerHeart(0.1f));
        currentArmor -= _damage;
        ShowPopUpDamage(_damage);
        if (currentArmor < 0)
        {
            currentHealth -= (_damage);
            FindObjectOfType<IndexBar>().OnDamage();
            currentArmor = 0;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
                Destroy(gameObject, 0.1f);
                return;
            }
        }
        if (regenArmorCoroutine != null)
        {
            StopCoroutine(regenArmorCoroutine);
        }
        regenArmorCoroutine = StartCoroutine(AfterTakeDamage());
        
    }
    private void ShowPopUpDamage(float damage)
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Vector3 randomOffset = new Vector3(Random.Range(-0.6f, 0.5f), Random.Range(-0.1f, 0.3f), 0);
        GameObject popup = Instantiate(damagePopupPrefab, transform.position + offset + randomOffset, Quaternion.identity);
        Destroy(popup, 0.5f);
    }
    IEnumerator DelayPLayerHeart(float timeDuration)
    {
        GameObject heart = Instantiate(playerHeart, transform.position, Quaternion.identity);
        heart.transform.localScale = transform.localScale;
        SpriteRenderer sr = heart.GetComponent<SpriteRenderer>();
        SpriteRenderer playerSR = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = playerSR.sprite;
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
    IEnumerator AfterTakeDamage()
    {
        canRengerArmor = false;
        yield return new WaitForSeconds(2f);
        canRengerArmor = true;
    }

    

}
