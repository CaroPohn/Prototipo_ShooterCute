using UnityEngine;

[CreateAssetMenu(fileName = "FlyingHShootState", menuName = "FlyingHSO/FlyingHShootState")]
public class FlyingHShootState : FlyingHStates
{
    public override void UpdateState(HeavyFlyingEnemy flyingHEnemy)
    {
        flyingHEnemy.ShootAnimationHandler();

        flyingHEnemy.SetLookAt();

        flyingHEnemy.GetComponent<FlyingHFSM>().ChangeState(flyingHEnemy.GetComponent<FlyingHFSM>().states[2]);
    }
}
