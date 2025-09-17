using UnityEngine;
using System.Linq;

public class Attack_Orientation : MonoBehaviour
{
    private float autoAimRadius = 8f;  
    [SerializeField] private LayerMask enemyLayer;      
    private Joystick aimJoystick;

    private void Start()
    {
        autoAimRadius = Player_Controller.Instance.autoAimRadius;
        enemyLayer = Player_Controller.Instance.enemyLayer;
        GameObject joystickObj = GameObject.Find("Variable Joystick");
        if (joystickObj != null)
            aimJoystick = joystickObj.GetComponent<Joystick>();
    }

    private void Update()
    {
        Vector2 aimDir = Vector2.zero;
        if (aimJoystick != null && aimJoystick.Horizontal != 0 && aimJoystick.Vertical != 0)
        {
            aimDir = new Vector2(aimJoystick.Horizontal, aimJoystick.Vertical);
        }

        if (aimDir.sqrMagnitude > 0.01f)
        {
            RotateTowardsDirection(aimDir);
            FlipSprite(aimDir.x);
        }
        else
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(
                Player_Controller.Instance.transform.position,
                autoAimRadius,
                enemyLayer
            );

            if (enemies.Length > 0)
            {
                Transform nearest = enemies
                    .OrderBy(e => Vector2.Distance(Player_Controller.Instance.transform.position, e.transform.position))
                    .First()
                    .transform;

                Vector2 direction = (nearest.position - Player_Controller.Instance.transform.position).normalized;
                RotateTowardsDirection(direction);
                FlipSprite(direction.x);
            }
        }
    }

    void RotateTowardsDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void FlipSprite(float dirX)
    {
        if (dirX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dirX < 0)
            transform.localScale = new Vector3(-1, -1, 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (Player_Controller.Instance != null)
        {
            Gizmos.DrawWireSphere(Player_Controller.Instance.transform.position, autoAimRadius);
        }
    }
}
