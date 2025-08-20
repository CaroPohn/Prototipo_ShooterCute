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

    //Called when the spawn animation is complete
    //void SpawnAnimationEnd()
    //{
    //    print("A");
    //}

    public void AttackPoseReached()
    {
        OnEnemyShooting?.Invoke();
    }

    public void OnFinishDeadAnimation()
    {
        patrolEnemy.stopDieAnimation = true;
    }
}
