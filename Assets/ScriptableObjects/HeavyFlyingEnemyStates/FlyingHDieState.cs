using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHDieState", menuName = "FlyingHSO/FlyingHDieState")]
public class FlyingHDieState : FlyingHStates
{
    public override void Enter(HeavyFlyingEnemy flyingHEnemy)
    {
        flyingHEnemy.StopFollowingPlayer(true);

        //patrolEnemy.DeactivateColliders();

        flyingHEnemy.DieAnimationHandler();
    }

    public override void UpdateState(HeavyFlyingEnemy flyingHEnemy)
    {
        if(flyingHEnemy.hasFallStarted)
            flyingHEnemy.Fall();
    }
}
