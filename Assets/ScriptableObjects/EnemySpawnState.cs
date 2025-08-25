using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnState", menuName = "Scriptable Objects/EnemySpawnState")]
public class EnemySpawnState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        HealthSystem healthSystem = patrolEnemy.GetComponent<HealthSystem>();

        patrolEnemy.SetHealthSystemActive(healthSystem, false);
    }

    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.SpawnAnimationHandler();

        patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[2]);
    }
}
