using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject weaponData;
    private GameObject player;
    public bool isPicked = false;
    private GameObject arrow;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            sprite.sortingOrder = 4;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (isPicked)
            {
                Debug.Log("Hello");
                DestroyArrow();
            }
            else
            {
                if (arrow == null)
                {
                    arrow = Instantiate(Resources.Load<GameObject>("ArrowIndicator"), transform.position + Vector3.up * 0.8f, Quaternion.Euler(0, 0, 180f), transform);
                    arrow.GetComponent<SpriteRenderer>().sortingOrder = 6;
                }
                if (WeaponInfoUI.instance != null)
                {
                    WeaponInfoUI.instance.ShowInfo(weaponData.GetComponent<WeaponBase>());
                }
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || isPicked)
        {
            DestroyArrow();
        }
    }
    public void DestroyArrow()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
            Destroy(arrow);
            arrow = null;
        }
        if (WeaponInfoUI.instance != null)
        {
            WeaponInfoUI.instance.HideInfo();
        }
    }
}
