using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]private Joystick joystick;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = PlayerHealth.instance.playerIndex.speed;
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
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
        }
        else if (rb.velocity.x < 0 || rb.velocity.y < 0)
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            anim.SetInteger("state", 0);
        }
    }
    

}
