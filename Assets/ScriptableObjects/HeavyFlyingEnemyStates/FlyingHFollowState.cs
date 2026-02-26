using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHFollowState", menuName = "FlyingHSO/FlyingHFollowState")]
public class FlyingHFollowState : FlyingHStates
{
    public override void Enter(HeavyFlyingEnemy flyingHEnemy)
    {
        flyingHEnemy.shootTimer = flyingHEnemy.timeBetweenShoots;
        flyingHEnemy.SetTargetToFollow();
    }

    public override void UpdateState(HeavyFlyingEnemy flyingHEnemy)
    {
        HealthSystem healthSystem = flyingHEnemy.GetComponent<HealthSystem>();

        flyingHEnemy.SetHealthSystemActive(healthSystem, true);

        flyingHEnemy.shootTimer -= Time.deltaTime;

        if (!flyingHEnemy.IsPlayerOnRange())
        {
            flyingHEnemy.StopFollowingPlayer(false);
        }
        else
        {
            flyingHEnemy.StopFollowingPlayer(true);

            if (healthSystem.health > 0)
                flyingHEnemy.SetLookAt();
        }

        if (flyingHEnemy.IsPlayerOnRange() == true && flyingHEnemy.shootTimer <= 0.0f)
        {
            flyingHEnemy.GetComponent<FlyingHFSM>().ChangeState(flyingHEnemy.GetComponent<FlyingHFSM>().states[3]);
        }

        if (healthSystem.health <= 0) 
        {
            flyingHEnemy.GetComponent<FlyingHFSM>().ChangeState(flyingHEnemy.GetComponent<FlyingHFSM>().states[4]);
        }
    }
}
