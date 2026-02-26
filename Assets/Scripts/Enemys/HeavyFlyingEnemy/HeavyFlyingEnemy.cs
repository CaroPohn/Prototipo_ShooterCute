using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyFlyingEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    public float shootTimer;
    [SerializeField] public float timeBetweenShoots;
    [SerializeField] private float damage;
    [SerializeField] private float projSpeed;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float flyingHeight;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private float stopDistance;

    //private List<Collider> enemyColliders;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //enemyColliders = new List<Collider>(GetComponentsInChildren<Collider>());

        agent.baseOffset = flyingHeight;
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        //MoveToTarget();
        //StopAgent();
    }

    //public void MoveToTarget()
    //{
    //    if (player != null && distanceToPlayer >= stopDistance) 
    //    { 
    //        agent.isStopped = false;
    //        agent.destination = player.position;
    //    }
    //}

    //public void StopAgent()
    //{
    //    if (distanceToPlayer <= stopDistance) 
    //    {
    //        agent.isStopped = true;
    //    }  
    //}

    public void SetHealthSystemActive(HealthSystem healthSystem, bool isActive)
    {
        healthSystem.enabled = isActive;
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = (player.position - shootPoint.position).normalized * Time.deltaTime;

        EnemyProjectile projScript = projectile.GetComponent<EnemyProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
            projScript.SetSpeed(projSpeed);
        }
    }

    //public void DeactivateColliders()
    //{
    //    foreach (Collider collider in enemyColliders)
    //    {
    //        collider.enabled = false;
    //    }
    //}

    public bool IsPlayerOnRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        return distanceToPlayer <= stopDistance;
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

        //if (!agent.isStopped)
        //{
        //    enemyAnimator.SetFloat("Velocity", 0.5f);
        //}
        //else
        //{
        //    enemyAnimator.SetFloat("Velocity", 0.0f);
        //}
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

    //public void ShootAnimationHandler()
    //{
    //    enemyAnimator.SetTrigger("Attack");
    //}

    //public void SpawnAnimationHandler()
    //{
    //    AkUnitySoundEngine.PostEvent("Enemy_Spawn_Adult", gameObject);

    //    StartCoroutine(SpawnCoroutine());

    //    Instantiate(spawnVFX, transform.position, transform.rotation);
    //}

    //public void DieAnimationHandler()
    //{
    //    StartCoroutine(DieCoroutine());
    //}

    //public IEnumerator DieCoroutine()
    //{
    //    while (!stopDieAnimation)
    //    {
    //        enemyAnimator.SetTrigger("Die");
    //        onStopDieAnimation?.Invoke(gameObject);

    //        yield return null;
    //    }
    //}

    //public IEnumerator SpawnCoroutine()
    //{
    //    enemyAnimator.SetTrigger("Spawn");

    //    yield return null;
    //}

    public IEnumerator SpawnCorroutine()
    {
        yield return null;
    }
}
