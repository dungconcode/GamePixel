using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance { get; private set; }
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]private Joystick joystick;
    public float speed;
    public bool isMoving = false;
    public Vector2 directionPlayer;

    public WeaponManager weaponManager;
    public WeaponBase currentWeapon;

    public float autoAimRadius = 8f;
    public LayerMask enemyLayer;

    private bool isKnockBack;
    private Player_KnockBack playerKnockBack;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerKnockBack = GetComponent<Player_KnockBack>();
        joystick = GameObject.Find("Variable Joystick").GetComponent<Joystick>();
        speed = PlayerHealth.instance.playerIndex.speed;

    }
    private void FixedUpdate()
    {
        if (!playerKnockBack.isKnockBack)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 inputDir = new Vector2(horizontal, vertical);

            if (inputDir.sqrMagnitude > 0.01f)
            {
                directionPlayer = inputDir.normalized;
            }
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
            Animation();
            Collider2D[] enemies = Physics2D.OverlapCircleAll(
                transform.position,
                autoAimRadius,
                enemyLayer
            );
            if (enemies.Length > 0)
            {
                Transform nearest = enemies
                        .OrderBy(e => Vector2.Distance(Player_Controller.Instance.transform.position, e.transform.position))
                        .First()
                        .transform;
                Vector2 direction = (nearest.position - transform.position).normalized;
                Flip(direction.x);
            }
            else if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip(directionPlayer.x);
            }
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }
    public void OnAttackButtonDown()
    {
        if (weaponManager != null)
        {
            weaponManager.currentWeapon.TryAttack();
        }
    }
    public void OnAttackButton2()
    {
        if (currentWeapon != null)
        {
            currentWeapon.TryAttack();
        }
    }
    public void OnSwitchWeapon()
    {
        if (weaponManager != null)
        {
            weaponManager.ChangeWeapon();
        }
    }
    void Flip(float dirX)
    {
        if (dirX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dirX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    void Animation()
    {
        if(rb.velocity.x > 0 || rb.velocity.y > 0)
        {
            anim.SetInteger("state", 1);
            isMoving = true;
        }
        else if (rb.velocity.x < 0 || rb.velocity.y < 0)
        {
            anim.SetInteger("state", 1);
            isMoving = true;
        }
        else
        {
            anim.SetInteger("state", 0);
            isMoving = false;
        }
    }
    
}
