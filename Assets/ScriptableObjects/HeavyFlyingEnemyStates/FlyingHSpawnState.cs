using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHSpawnState", menuName = "FlyingHSO/FlyingHSpawnState")]
public class FlyingHSpawnState : FlyingHStates
{
    public override void Enter(HeavyFlyingEnemy flyingHEnemy)
    {
        HealthSystem healthSystem = flyingHEnemy.GetComponent<HealthSystem>();

        flyingHEnemy.SetHealthSystemActive(healthSystem, false);

        flyingHEnemy.agent.enabled = false;

        flyingHEnemy.SpawnAnimationHandler();
    }

    public override void UpdateState(HeavyFlyingEnemy flyingHEnemy)
    {
        flyingHEnemy.StopFollowingPlayer(true);

        if (flyingHEnemy.hasReachTop)
        {
            flyingHEnemy.StartDescend();
        }

        if (flyingHEnemy.hasReachSurface) 
        {
            flyingHEnemy.GetComponent<FlyingHFSM>().ChangeState(flyingHEnemy.GetComponent<FlyingHFSM>().states[2]);
        }
    }
}
