using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailDragon : MonoBehaviour
{
    public Transform followPos;
    private float moveSpeed = 17f;
    private float followDistance = 0.4f;

    private SpriteRenderer sprite;
    private float fadeDuration = 1f; // thời gian fade in

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color c = sprite.color;
        c.a = 0f; // bắt đầu mờ
        sprite.color = c;
    }

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        Color c = sprite.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            sprite.color = c;
            yield return null;
        }

        c.a = 1f;
        sprite.color = c;
    }

    private void FixedUpdate()
    {
        if (followPos == null)
            return;

        float dis = Vector3.Distance(transform.position, followPos.position);
        if (dis > followDistance)
        {
            transform.position = Vector3.Lerp(transform.position, followPos.position, moveSpeed * Time.deltaTime);
            Vector3 dir = followPos.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
