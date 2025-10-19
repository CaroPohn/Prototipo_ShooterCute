using System;
using System.Diagnostics;
using UnityEngine;

public class MeleeAnimationHandler : MonoBehaviour
{
    private MeleeEnemy meleeEnemy;

    public event Action OnEnemyAttacking;

    private void Start()
    {
        meleeEnemy = GetComponentInParent<MeleeEnemy>();
    }

    public void AttackPoseReached()
    {
        OnEnemyAttacking?.Invoke();
    }

    public void OnFinishDeadAnimation()
    {
        meleeEnemy.stopMeleeDieAnimation = true;
    }

    public void OnFinishSpawnAnimation()
    {
        meleeEnemy.stopMeleeSpawnAnimation = true;
    }
}
