using System;
using System.Diagnostics;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private PatrolEnemy patrolEnemy;

    public event Action OnEnemyShooting;

    private void Start()
    {
        patrolEnemy = GetComponentInParent<PatrolEnemy>();
    }

    public void AttackPoseReached()
    {
        OnEnemyShooting?.Invoke();
    }

    public void OnFinishDeadAnimation()
    {
        patrolEnemy.stopDieAnimation = true;
    }

    public void OnFinishSpawnAnimation()
    {
        patrolEnemy.stopSpawnAnimation = true;
    }
}
