using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance;
    public Transform respawnPoint;

    public void Awake(){
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            Debug.Log("chạm");
            collision.transform.position = respawnPoint.position;
            Debug.Log("đã di chuyển");
        }
    }
}
