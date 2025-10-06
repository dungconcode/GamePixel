using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Trap/SpawnToxicArea")]
public class SpawnToxicArea : ScriptableObject, ITrapEffect
{
    [SerializeField] private GameObject toxicAreaPrefab;

    public void ApplyEffect(Vector3 position)
    {
        GameObject toxicArea = Instantiate(toxicAreaPrefab, position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 scale = toxicArea.transform.localScale;

            if (player.transform.position.x < position.x)
                scale.x = -Mathf.Abs(scale.x); // player bên trái → quay sang trái
            else
                scale.x = Mathf.Abs(scale.x);  // player bên phải → quay sang phải

            toxicArea.transform.localScale = scale;
        }
    }
}