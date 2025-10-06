using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrapEffect
{
    public EnemyType enemyType;
    public ScriptableObject effectSO;
    private ITrapEffect _effect;

    public ITrapEffect Effect
    {
        get
        {
            if (_effect == null && effectSO != null)
                _effect = effectSO as ITrapEffect;
            return _effect;
        }
    }
}
public class EnemyTrapDeath : MonoBehaviour
{
    

    public TrapEffect trapEffect;
    private void OnEnable()
    {
        Debug.Log("Subscribe HandleEnemyDeath");
        Enemy_Health.OnEnemyDeath -= HandleEnemyDeath;
        Enemy_Health.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        Enemy_Health.OnEnemyDeath -= HandleEnemyDeath;
    }
    private void HandleEnemyDeath(Enemy_Health deadEnemy, Vector3 position, EnemyType enemyType)
    {
        if (deadEnemy.gameObject != gameObject) return;
        if (trapEffect == null || trapEffect.Effect == null) return;
        if (enemyType == trapEffect.enemyType && trapEffect.Effect != null)
        {
            trapEffect.Effect.ApplyEffect(position);
        }
    }
}
