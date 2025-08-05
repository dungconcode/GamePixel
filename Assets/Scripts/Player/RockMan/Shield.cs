using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float expandDuration = 0.2f;
    public float stayDuration = 2f;
    private float shrinkDuration = 0.2f;
    private float targetScale = 1.5f; 
    public Transform visual; 

    [SerializeField]private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
        StartCoroutine(AnimateShield());
    }

    IEnumerator AnimateShield()
    {
        yield return StartCoroutine(ScaleOverTime(Vector3.zero, Vector3.one * targetScale, expandDuration));
        if(playerHealth.currentArmor < playerHealth.maxArmor)
        {
            playerHealth.currentArmor += 2;
            if(playerHealth.currentArmor > playerHealth.maxArmor)
            {
                playerHealth.currentArmor = playerHealth.maxArmor;
            }
        }
        yield return new WaitForSeconds(stayDuration);
        yield return StartCoroutine(ScaleOverTime(Vector3.one * targetScale, Vector3.zero, shrinkDuration));
        Destroy(gameObject);
    }
    
    IEnumerator ScaleOverTime(Vector3 from, Vector3 to, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            visual.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }
        visual.localScale = to;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
