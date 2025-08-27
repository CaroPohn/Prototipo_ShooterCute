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

    [SerializeField] private EnemyAnimationHandler enemyAnimationHandler;

    public float shootCoolDown;
    public float damage;
    public float shootTimer;

    public bool stopDieAnimation;

    private NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        stopDieAnimation = false;
    }

    private void OnEnable()
    {
        enemyAnimationHandler.OnEnemyShooting += ShootLogic;
    }

    private void OnDisable()
    {
        enemyAnimationHandler.OnEnemyShooting -= ShootLogic;
    }

    public void ShootLogic()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = (player.position - shootPoint.position).normalized * Time.deltaTime;

        EnemyProjectile projScript = projectile.GetComponent<EnemyProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
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
        StartCoroutine(SpawnCoroutine());
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