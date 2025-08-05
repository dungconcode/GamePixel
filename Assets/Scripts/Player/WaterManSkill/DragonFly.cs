using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragonFly : MonoBehaviour
{
    public Transform player;
    public Transform centerPoint;
    public float a = 4f;
    public float b = 4f;
    public float speed = 2f;
    private float timeElapsed = 0f;

    public int flip = 1;
    public void Init(Transform player)
    {
        this.player = player;
        flip = player.localScale.x > 0 ? 1 : -1; // Lấy hướng của player
    }
    void Update()
    {
        timeElapsed += Time.deltaTime * speed;
        float x = flip * a * Mathf.Sin(timeElapsed);
        float y = b * Mathf.Sin(timeElapsed) * Mathf.Cos(timeElapsed); // số 8

        Vector3 offset = new Vector3(x, y, 0f);
        transform.position = centerPoint.position + offset;

        float dx = flip * a * Mathf.Cos(timeElapsed);
        float dy = b * (Mathf.Cos(timeElapsed) * Mathf.Cos(timeElapsed) - Mathf.Sin(timeElapsed) * Mathf.Sin(timeElapsed));

        Vector3 dir = new Vector3(dx, dy, 0f);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
