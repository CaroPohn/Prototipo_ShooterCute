using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    [Header("Follow")]
    private Transform player;

    public float attackDistance;

    [Header("Shoot")]
    [SerializeField] private Transform shootPoint;

    private List<Collider> enemyColliders;

    [SerializeField] private MeleeAnimationHandler meleeAnimationHandler;

    [SerializeField] private GameObject spawnVFX;

    public float attackCoolDown;
    public float damage;
    public float attackTimer;

    public bool stopMeleeDieAnimation;
    public bool stopMeleeSpawnAnimation;

    public float attackRadius;

    public static event Action<GameObject> onStopDieAnimation;

    private NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyColliders = new List<Collider>(GetComponentsInChildren<Collider>());

        agent = GetComponent<NavMeshAgent>();

        stopMeleeDieAnimation = false;
        stopMeleeSpawnAnimation = false;
    }

    private void OnEnable()
    {
        meleeAnimationHandler.OnEnemyAttacking += MeleeLogic;
        meleeAnimationHandler.OnEnemyStep += StepSoundActivation;
    }

    private void OnDisable()
    {
        meleeAnimationHandler.OnEnemyAttacking -= MeleeLogic;
        meleeAnimationHandler.OnEnemyStep -= StepSoundActivation;
    }

    public void StepSoundActivation()
    {
        AkUnitySoundEngine.PostEvent("Enemy_Footstep_Adult", gameObject);
    }

    public void MeleeLogic()
    {
        Collider[] hitColliders = Physics.OverlapSphere(shootPoint.position, attackRadius);

        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealthSystem playerHealth = hit.GetComponent<PlayerHealthSystem>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    playerHealth.SetEffectType(PlayerHealthSystem.EffectType.EnemyDamage);
                }
            }
        }
    }

    public void DeactivateColliders()
    {
        foreach (Collider collider in enemyColliders)
        {
            collider.enabled = false;
        }
    }

    public bool IsPlayerOnRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        return distanceToPlayer <= attackDistance;
    }

    public void SetTargetToFollow()
    {
        agent.SetDestination(player.position);
    }

    public void StopFollowingPlayer(bool doesEnemyhasToStop)
    {
        agent.isStopped = doesEnemyhasToStop;

        if (!doesEnemyhasToStop)
        {
            SetTargetToFollow();
        }

        if (!agent.isStopped)
        {
            enemyAnimator.SetFloat("Velocity", 0.8f);
        }
        else
        {
            enemyAnimator.SetFloat("Velocity", 0.0f);
        }
    }

    public void SetLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        if (vecLookAt != Vector3.zero)
        {
            transform.forward = vecLookAt.normalized;
        }
    }

    public void SetHealthSystemActive(HealthSystem healthSystem, bool isActive)
    {
        healthSystem.enabled = isActive;
    }

    public void AttackAnimationHandler()
    {
        enemyAnimator.SetTrigger("Attack");
    }

    public void SpawnAnimationHandler()
    {
        AkUnitySoundEngine.PostEvent("Enemy_Spawn_Adult", gameObject);

        StartCoroutine(SpawnCoroutine());

        Instantiate(spawnVFX, transform.position, transform.rotation);
    }

    public void DieAnimationHandler()
    {
        StartCoroutine(DieCoroutine());
    }

    public IEnumerator DieCoroutine()
    {
        while (!stopMeleeDieAnimation)
        {
            enemyAnimator.SetTrigger("Die");
            onStopDieAnimation?.Invoke(gameObject);

            yield return null;
        }
    }

    public IEnumerator SpawnCoroutine()
    {
        enemyAnimator.SetTrigger("Spawn");

        yield return null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (shootPoint != null)
            Gizmos.DrawWireSphere(shootPoint.position, attackRadius);
    }
}
