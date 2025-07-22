using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_SeedBullet : MonoBehaviour
{
    [SerializeField] private Transform actionPoint;
    [SerializeField] private GameObject bulletPrefab;
     public float speed;
    [SerializeField] private bool isShooting = false;
    public void ActiveSkill(float timeToDelaySkill)
    {
        if (isShooting) return; // Prevent multiple shots at the same time
        StartCoroutine(WaitForNextSkill(timeToDelaySkill));
    }
    IEnumerator WaitForNextSkill(float timeToDelay)
    {
        isShooting = true;

        float[] angles = { -15f, 0f, 15f }; // 3 góc bắn: trái, giữa, phải

        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, actionPoint.eulerAngles.z + angle);
            GameObject airKnight = Instantiate(bulletPrefab, actionPoint.position, rotation);
            Rigidbody2D rb = airKnight.GetComponent<Rigidbody2D>();
            rb.AddForce(rotation * Vector2.right * speed, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(timeToDelay);
        isShooting = false;
    }
}
