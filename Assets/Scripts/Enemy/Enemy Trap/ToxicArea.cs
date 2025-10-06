using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player_Collision playerCollision = collision.GetComponent<Player_Collision>();
            if(playerCollision != null)
            {
                playerCollision.StartToxicArea(5f, 1f, 1); // Duration: 5 seconds, Interval: 1 second, Damage per tick: 10
            }
        }
    }
}
