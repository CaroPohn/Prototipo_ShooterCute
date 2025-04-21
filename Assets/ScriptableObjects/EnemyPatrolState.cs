using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPatrolState", menuName = "Scriptable Objects/EnemyPatrolState")]
public class EnemyPatrolState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.MoveToNextPoint();
    }
}
