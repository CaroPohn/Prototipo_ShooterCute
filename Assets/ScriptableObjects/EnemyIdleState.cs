using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIdleState", menuName = "Scriptable Objects/EnemyIdleState")]
public class EnemyIdleState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {

    }

    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[1]);
    }
}
