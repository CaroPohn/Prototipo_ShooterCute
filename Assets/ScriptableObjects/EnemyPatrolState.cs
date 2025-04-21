using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPatrolState", menuName = "Scriptable Objects/EnemyPatrolState")]
public class EnemyPatrolState : EnemyStates
{
    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.MoveToNextPoint();

        if (patrolEnemy.IsPlayerOnRange() == true)
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[2]);
        }
    }
}
