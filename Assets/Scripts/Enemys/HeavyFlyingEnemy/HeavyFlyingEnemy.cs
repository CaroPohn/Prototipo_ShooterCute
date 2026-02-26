using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyFlyingEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
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

    [SerializeField] private float jumpHight;
    [SerializeField] private float ascentTime;
    public bool hasReachTop;
    public bool hasReachSurface;

    [SerializeField] private float goToSurfaceSpeed;
    [SerializeField] private LayerMask navMeshSurfaceLayer;

    //private List<Collider> enemyColliders;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //enemyColliders = new List<Collider>(GetComponentsInChildren<Collider>());

        hasReachTop = false;
        hasReachSurface = false;

        agent.baseOffset = flyingHeight;
        agent.speed = moveSpeed;
    }

    private void Update()
    {
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
        if (agent.enabled == true)
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

    public void SpawnAnimationHandler()
    {
        StartCoroutine(AscenderCorroutine());

        //Instantiate(spawnVFX, transform.position, transform.rotation);
    }

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

    private IEnumerator AscenderCorroutine()
    {
        Vector3 initialPosition = transform.position;
        Vector3 objectivePosition = initialPosition + Vector3.up * jumpHight;
        float timer = 0f;

        while (timer < ascentTime)
        {
            transform.position = Vector3.Lerp(initialPosition, objectivePosition, timer / ascentTime);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = objectivePosition;
        hasReachTop = true;
    }

    public void StartDescend()
    {
        if (hasReachTop)
        {
            hasReachTop = false;
            StartCoroutine(DescenderCorroutine());
        }
    }

    private IEnumerator DescenderCorroutine()
    {
        Vector3 destinationPoint = transform.position;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitpoint, Mathf.Infinity, navMeshSurfaceLayer))
        {
            if (NavMesh.SamplePosition(hitpoint.point, out NavMeshHit hitNavMesh, 5f, NavMesh.AllAreas))
            {
                destinationPoint = hitNavMesh.position;
            }
            else
            {
                destinationPoint = hitpoint.point;
            }
        }
        else
        {
            yield break;
        }

        while (Vector3.Distance(transform.position, destinationPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, goToSurfaceSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destinationPoint;
        agent.enabled = true;
        hasReachSurface = true;
    }

public IEnumerator SpawnCoroutine()
    {
        //enemyAnimator.SetTrigger("Spawn");

        yield return null;
    }

    public IEnumerator SpawnCorroutine()
    {
        yield return null;
    }
}
