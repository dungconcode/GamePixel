using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Rotate_Weapon : MonoBehaviour
{
    [SerializeField] Transform enemyTransforms;
    [SerializeField] Transform playerTransforms;
    [SerializeField] private float detectionRadius = 1f;

    public int maxAngle;
    private void Start()
    {
        enemyTransforms = transform.parent;
        playerTransforms = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        CheckPlayerNearly();

    }
    private void CheckPlayerNearly()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransforms.position - transform.position, detectionRadius, 1 << LayerMask.NameToLayer("Player"));
        Collider2D radiusCheck = Physics2D.OverlapCircle(transform.position, detectionRadius, 1 << LayerMask.NameToLayer("Player"));
        if (hit.collider != null)
        {
            Vector2 dir = (playerTransforms.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
            float tmp = 1;
            if (enemyTransforms.localScale.x < 0)
            {
                tmp = -1;
            }
            else
            {
                tmp = 1;
            }
            if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            {
                transform.localScale = new Vector3(1 * tmp, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1 * tmp, 1, 1);
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            transform.rotation = enemyTransforms.rotation;
            transform.localScale = enemyTransforms.localScale;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
