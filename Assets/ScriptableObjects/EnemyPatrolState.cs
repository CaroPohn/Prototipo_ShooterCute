using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPatrolState", menuName = "Scriptable Objects/EnemyPatrolState")]
public class EnemyPatrolState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.shootTimer = patrolEnemy.shootCoolDown;
    }

    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.MoveToNextPoint();

        patrolEnemy.shootTimer -= Time.deltaTime;

        if (patrolEnemy.IsPlayerOnRangeToFollowPlayer() == true)
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[2]);
        }

        if (patrolEnemy.IsPlayerOnRangeToFollowPlayer() == true && patrolEnemy.shootTimer <= 0.0f)
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[3]);
        }
    }
}
