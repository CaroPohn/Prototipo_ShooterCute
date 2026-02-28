using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHDieState", menuName = "FlyingHSO/FlyingHDieState")]
public class FlyingHDieState : FlyingHStates
{
    public override void Enter(HeavyFlyingEnemy patrolEnemy)
    {
        patrolEnemy.StopFollowingPlayer(true);

        //patrolEnemy.DeactivateColliders();

        //patrolEnemy.DieAnimationHandler();
    }
}
