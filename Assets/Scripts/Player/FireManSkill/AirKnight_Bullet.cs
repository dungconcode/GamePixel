using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKnight_Bullet : MonoBehaviour
{
    private float timeCoolDown = 3f;

    private void Update()
    {
        timeCoolDown -= Time.deltaTime;
        if (timeCoolDown <= 0)
        {
            Destroy(gameObject);
        }
    }
}
