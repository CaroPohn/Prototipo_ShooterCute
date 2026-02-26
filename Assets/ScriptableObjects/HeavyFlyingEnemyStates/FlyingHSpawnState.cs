using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHSpawnState", menuName = "FlyingHSO/FlyingHSpawnState")]
public class FlyingHSpawnState : FlyingHStates
{
    public override void Enter(HeavyFlyingEnemy flyingHEnemy)
    {
        HealthSystem healthSystem = flyingHEnemy.GetComponent<HealthSystem>();

        flyingHEnemy.SetHealthSystemActive(healthSystem, false);

        //patrolEnemy.SpawnAnimationHandler();
    }

    public override void UpdateState(HeavyFlyingEnemy flyingHEnemy)
    {
        flyingHEnemy.StopFollowingPlayer(true);

        //if (patrolEnemy.stopSpawnAnimation)
        //{
        flyingHEnemy.GetComponent<FlyingHFSM>().ChangeState(flyingHEnemy.GetComponent<FlyingHFSM>().states[2]);
        //}
    }
}
