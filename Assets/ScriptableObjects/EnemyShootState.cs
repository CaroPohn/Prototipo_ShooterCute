using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShootState", menuName = "Scriptable Objects/EnemyShootState")]
public class EnemyShootState : EnemyStates
{
    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.Shoot();

        patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[2]);
    }
}
