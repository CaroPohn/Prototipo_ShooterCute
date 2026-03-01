using UnityEngine;

[CreateAssetMenu(fileName = "MeleeFollowState", menuName = "MeleeSO/MeleeFollowState")]
public class MeleeFollowState : MeleeStates
{
    public override void Enter(MeleeEnemy meleeEnemy)
    {
        meleeEnemy.attackTimer = meleeEnemy.attackCoolDown;
        meleeEnemy.SetTargetToFollow();
    }

    public override void UpdateState(MeleeEnemy meleeEnemy)
    {
        HealthSystem healthSystem = meleeEnemy.GetComponent<HealthSystem>();

        meleeEnemy.SetHealthSystemActive(healthSystem, true);

        meleeEnemy.attackTimer -= Time.deltaTime;

        if (meleeEnemy.frostTest)
        {
            meleeEnemy.FreezeEnemyEffect();
        }
        else
        {
            meleeEnemy.StopFreezeEffect();
        }

        if (!meleeEnemy.IsPlayerOnRange())
        {
            meleeEnemy.StopFollowingPlayer(false);
        }
        else
        {
            meleeEnemy.StopFollowingPlayer(true);

            if (healthSystem.health > 0)
                meleeEnemy.SetLookAt();
        }

        if (meleeEnemy.IsPlayerOnRange() == true && meleeEnemy.attackTimer <= 0.0f)
        {
            meleeEnemy.GetComponent<MeleeFSM>().ChangeState(meleeEnemy.GetComponent<MeleeFSM>().states[3]);
        }

        if (healthSystem.health <= 0)
        {
            meleeEnemy.GetComponent<MeleeFSM>().ChangeState(meleeEnemy.GetComponent<MeleeFSM>().states[4]);
        }
    }
}
