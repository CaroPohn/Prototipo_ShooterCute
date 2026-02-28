using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHIdleState", menuName = "FlyingHSO/FlyingHIdleState")]
public class FlyingHIdleState : FlyingHStates
{
    public override void Enter(HeavyFlyingEnemy flyingHEnemy)
    {
        flyingHEnemy.GetComponent<FlyingHFSM>().ChangeState(flyingHEnemy.GetComponent<FlyingHFSM>().states[1]);
    }
}