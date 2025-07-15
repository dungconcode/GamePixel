using Unity.Burst.CompilerServices;
using UnityEngine;

public class Attack_Orientation: MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        Vector2 moveDir = rb.velocity;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            RotateTowardsMovement(moveDir);
        }
        if(moveDir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }else if(moveDir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void RotateTowardsMovement(Vector2 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
