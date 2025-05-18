using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFollowState", menuName = "Scriptable Objects/EnemyFollowState")]
public class EnemyFollowState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.shootTimer = patrolEnemy.shootCoolDown;
    }

    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.shootTimer -= Time.deltaTime;

        if (!patrolEnemy.IsPlayerOnRange())
        {
            patrolEnemy.FollowPlayer();
        }
        
        patrolEnemy.SetLookAt();

        if (patrolEnemy.IsPlayerOnRange() == true && patrolEnemy.shootTimer <= 0.0f)
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[2]);
        }

        HealthSystem healthSystem = patrolEnemy.GetComponent<HealthSystem>();

        if (healthSystem.health <= 0) 
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[0]);
        }
    }
}
