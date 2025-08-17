using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_SkyStrike : SkillBase
{
    public float radius = 4f;
    private float targetEnemy = 4f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject effectPrefabs;
    [SerializeField] private GameObject indicatorPrefabs;
    public float indicatorDuration = 0.1f;
    public float delayBeforeHit = 0.2f;
    private void Start()
    {
        radius = 4f; // Set default radius if not set in inspector
    }
    public override void Activate()
    {
        ActiveSkill();
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
                GameObject indicator = Instantiate(indicatorPrefabs, enemy.transform.position, Quaternion.identity);
                Destroy(indicator, indicatorDuration); // Destroy indicator after a short duration
                yield return new WaitForSeconds(indicatorDuration);
                GameObject efect = Instantiate(effectPrefabs, enemy.transform.position, Quaternion.identity);
                Destroy(efect, 0.5f); // Destroy effect after 1 second
                yield return new WaitForSeconds(delayBeforeHit);
                count++;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
