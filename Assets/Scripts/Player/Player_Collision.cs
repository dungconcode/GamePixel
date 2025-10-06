using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    #region ToxicArea

    private Coroutine toxicRoutine;
    public void StartToxicArea(float duration, float interval, int damagePerTick)
    {
        if(toxicRoutine != null)
        {
            StopCoroutine(toxicRoutine);
        }
        toxicRoutine = StartCoroutine(ToxicDamage(duration, interval, damagePerTick));
    }
    IEnumerator ToxicDamage(float duration, float interval, int damagePerTick)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            PlayerHealth.instance.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
        toxicRoutine = null;
    }
    #endregion
}
