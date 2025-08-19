using System.Diagnostics;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private PatrolEnemy patrolEnemy;

    private void Start()
    {
        patrolEnemy = GetComponentInParent<PatrolEnemy>();
    }

    //Called when the spawn animation is complete
    void SpawnAnimationEnd()
    {
        print("A");
    }
    //Called when the enemy is at the desired attack positon (for example, mouth open)
    void AttackPoseReached()
    {
        print("B");
    }

    public void OnFinishDeadAnimation()
    {
        patrolEnemy.stopDieAnimation = true;
    }
}
