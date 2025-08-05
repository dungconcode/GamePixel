using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 moveDir = rb.velocity;

        // Nếu đang di chuyển thì xoay theo hướng đó
        if (moveDir.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
        }
    }

    void RotateTowardsMovement(Vector2 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
