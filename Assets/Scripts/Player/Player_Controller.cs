using System.Collections;
using System.Collections.Generic;
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
        joystick = GameObject.Find("Variable Joystick").GetComponent<Joystick>();
        speed = PlayerHealth.instance.playerIndex.speed;

    }
    private void FixedUpdate()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(horizontal, vertical);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            directionPlayer = inputDir.normalized;
        }
        //horizontal = joystick.Horizontal;
        //vertical = joystick.Vertical;
        Animation();
        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
