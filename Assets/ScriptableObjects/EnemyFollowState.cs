using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFollowState", menuName = "Scriptable Objects/EnemyFollowState")]
public class EnemyFollowState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.shootTimer = patrolEnemy.shootCoolDown;
        patrolEnemy.SetTargetToFollow();
    }

    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        HealthSystem healthSystem = patrolEnemy.GetComponent<HealthSystem>();

        patrolEnemy.SetHealthSystemActive(healthSystem, true);

        patrolEnemy.shootTimer -= Time.deltaTime;

        if (!patrolEnemy.IsPlayerOnRange())
        {
            patrolEnemy.StopFollowingPlayer(false);
        }
        else
        {
            patrolEnemy.StopFollowingPlayer(true);

            if (healthSystem.health > 0)
                patrolEnemy.SetLookAt();
        }

        if (patrolEnemy.IsPlayerOnRange() == true && patrolEnemy.shootTimer <= 0.0f)
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[3]);
        }

        if (healthSystem.health <= 0) 
        {
            patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[4]);
        }
    }
}
