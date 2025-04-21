using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPatrolState", menuName = "Scriptable Objects/EnemyPatrolState")]
public class EnemyPatrolState : EnemyStates
{
    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.MoveToNextPoint();
    }
}
