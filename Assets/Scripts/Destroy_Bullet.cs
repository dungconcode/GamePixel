using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Bullet : MonoBehaviour
{
    private float timeCoolDown = 2f;

    private void Update()
    {
        timeCoolDown -= Time.deltaTime;
        if (timeCoolDown <= 0)
        {
            Destroy(gameObject);
        }
    }
}
