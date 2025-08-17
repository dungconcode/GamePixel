using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public float moveUpSpeed = 1f;
    public float disappearTime = 1f;
    private TMP_Text textMesh;
    private Color textColor;
    private float timer;

    public void Setup(float damage)
    {
        
        textMesh = GetComponent<TMP_Text>();
        textMesh.text = damage.ToString();
        textColor = textMesh.color;
        timer = disappearTime;
        if (textMesh == null)
        {
            Debug.LogError("textMesh is NULL — TMP_Text not found!");
            return;
        }
    }

    private void Update()
    {
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            textColor.a -= 2f * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
