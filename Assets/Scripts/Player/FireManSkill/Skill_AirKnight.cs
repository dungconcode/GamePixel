using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AirKnight : MonoBehaviour
{
    [SerializeField] private Transform actionPoint;
    [SerializeField] private GameObject skillPrefab;
    public float speed;
    [SerializeField] private bool isShooting = false;
    public void ActiveSkill(float timeToDelaySkill)
    {
        if(isShooting) return; // Prevent multiple shots at the same time
        StartCoroutine(WaitForNextSkill(timeToDelaySkill));
    }
    IEnumerator WaitForNextSkill(float timetoDelay)
    {
        Debug.Log("Air Knight Skill Activated");
        isShooting = true;
        GameObject airKnight = Instantiate(skillPrefab, actionPoint.position, actionPoint.rotation);
        Rigidbody2D rb = airKnight.GetComponent<Rigidbody2D>();
        rb.AddForce(actionPoint.right * speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(timetoDelay);
        isShooting = false;
    }
    private void OnDisable()
    {
        isShooting = false; // Reset shooting state when skill is disabled
    }
}
