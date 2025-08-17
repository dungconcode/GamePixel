using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotate_Weapon : MonoBehaviour
{
    [SerializeField] Transform enemyTransforms;
    [SerializeField] Transform playerTransforms;

    private void Start()
    {
        enemyTransforms = transform.parent;
        playerTransforms = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        Vector2 dir = (playerTransforms.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float tmp = 1;
        if(enemyTransforms.localScale.x < 0)
        {
            tmp = -1;
        }
        else
        {
            tmp = 1;
        }
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1 * tmp, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1 * tmp, 1, 1);
        }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
