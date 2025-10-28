using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    [Header("Follow")]
    private Transform player;

    public float followDistance;

    [Header("Shoot")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;

    private List<Collider> enemyColliders;

    [SerializeField] private EnemyAnimationHandler enemyAnimationHandler;

    [SerializeField] private GameObject spawnVFX;

    public float shootCoolDown;
    public float damage;
    public float shootTimer;

    public bool stopDieAnimation;
    public bool stopSpawnAnimation;

    public static event Action<GameObject> onStopDieAnimation;

    private NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyColliders = new List<Collider>(GetComponentsInChildren<Collider>());

        agent = GetComponent<NavMeshAgent>();

        stopDieAnimation = false;
        stopSpawnAnimation = false;
    }

    private void OnEnable()
    {
        enemyAnimationHandler.OnEnemyShooting += ShootLogic;
        enemyAnimationHandler.OnEnemyStep += StepSoundActivation;
    }

    private void OnDisable()
    {
        enemyAnimationHandler.OnEnemyShooting -= ShootLogic;
        enemyAnimationHandler.OnEnemyStep -= StepSoundActivation;
    }

    public void StepSoundActivation()
    {
        AkUnitySoundEngine.PostEvent("Enemy_Footstep_Adult", gameObject);
    }

    public void ShootLogic()
    {
        AkUnitySoundEngine.PostEvent("Enemy_Shoot_Basic", gameObject);

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = (player.position - shootPoint.position).normalized * Time.deltaTime;

        EnemyProjectile projScript = projectile.GetComponent<EnemyProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
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

        return distanceToPlayer <= followDistance;
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
            enemyAnimator.SetFloat("Velocity", 0.5f);
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

    public void ShootAnimationHandler()
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
        while (!stopDieAnimation)
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}