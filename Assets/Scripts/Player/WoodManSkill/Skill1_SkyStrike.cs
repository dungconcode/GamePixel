using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_SkyStrike : MonoBehaviour
{
    public float radius = 4f;
    private float targetEnemy = 4f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject effectPrefabs;
    public float delayBeforeHit = 0.2f;
    private void Start()
    {
        radius = 4f; // Set default radius if not set in inspector
    }
    public void ActiveSkill()
    {
        StartCoroutine(ExecuteStrike());
    }
    private IEnumerator ExecuteStrike()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        List<Collider2D> sortEnemy = new List<Collider2D>(enemis);
        sortEnemy.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position)
                   .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
        int count = 0;
        foreach (Collider2D enemy in sortEnemy)
        {
            if (count >= targetEnemy) break;
            if (enemy != null)
            {
                GameObject efect = Instantiate(effectPrefabs, enemy.transform.position, Quaternion.identity);
                Destroy(efect, 1f); // Destroy effect after 1 second
                yield return new WaitForSeconds(delayBeforeHit);
                count++;
            }
            else Debug.Log("Chạy cụ mày lõi rồi");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
