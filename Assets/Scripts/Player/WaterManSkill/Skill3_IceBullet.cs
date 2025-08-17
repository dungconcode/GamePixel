using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_IceBullet : SkillBase
{
    [SerializeField] private GameObject iceBulletPrefab;
    private Transform playerTransform;
    private float bulletSpeed = 10f;
    private int bulletCount = 10;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void Activate()
    {
        ActiveSkill();
    }
    public void ActiveSkill()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            float rad = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            GameObject iceBullet = Instantiate(iceBulletPrefab, playerTransform.position, Quaternion.identity);
            iceBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Rigidbody2D rb = iceBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
