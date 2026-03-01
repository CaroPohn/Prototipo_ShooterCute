using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    [SerializeField] private float fallSpeedSetter = 18f;
    private float fallSpeed = 0f;
    [SerializeField] private float fallCollisionRadius = 0.5f;
    [SerializeField] private GameObject explotionPrefab;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private float stopDistance;

    public bool hasReachTop;
    public bool hasReachSurface;

    [SerializeField] private float goToSurfaceTime;
    [SerializeField] private float goToLayerSpeed;
    [SerializeField] private LayerMask navMeshSurfaceLayer;

    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private HeavyFlyingEnemyAnimatorHandler animatorHandler;

    public bool hasFallStarted;

    private void OnEnable()
    {
        animatorHandler.OnFlyingHAttack += Shoot;
        animatorHandler.OnFlyingFall += DeactivateNavMesh;
        animatorHandler.OnFlyingFall += StartFall;
    }

    private void OnDisable()
    {
        animatorHandler.OnFlyingHAttack -= Shoot;
        animatorHandler.OnFlyingFall -= DeactivateNavMesh;
        animatorHandler.OnFlyingFall -= StartFall;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        hasReachTop = false;
        hasReachSurface = false;
        hasFallStarted = false;

        agent.speed = moveSpeed;
    }

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

    public void DeactivateNavMesh()
    {
        agent.enabled = false;
    }

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

        if (!agent.isStopped)
        {
            enemyAnimator.SetFloat("Speed", 1f);
        }
        else
        {
            enemyAnimator.SetFloat("Speed", 0.0f);
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

    public void ShootAnimationHandler()
    {
        enemyAnimator.SetTrigger("Attack");
    }

    public void SpawnAnimationHandler()
    {
        StartCoroutine(AscenderCorroutine());
        StartCoroutine(SpawnCoroutine());

        //Instantiate(spawnVFX, transform.position, transform.rotation);
    }

    public void DieAnimationHandler()
    {
        enemyAnimator.SetTrigger("Death");
    }

    private void StartFall()
    {
        hasFallStarted = true;
    }

    public void Fall()
    {
        fallSpeed += fallSpeedSetter * Time.deltaTime;
        float movementDistance = fallSpeed;

        transform.Translate(Vector3.down * movementDistance * Time.deltaTime, Space.World);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, fallCollisionRadius);

        bool hasCrushed = false;

        foreach (Collider collider in hitColliders)
        {
            if (collider.transform.root != this.transform.root && !collider.isTrigger)
            {
                hasCrushed = true;
                break;
            }
        }

        if (hasCrushed)
        {
            if (explotionPrefab != null)
            {
                Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator AscenderCorroutine()
    {
        Vector3 destinationPoint = transform.position;
        float timer = 0f;

        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hitpoint, Mathf.Infinity, navMeshSurfaceLayer))
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

        while (timer <= goToSurfaceTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destinationPoint, timer / goToSurfaceTime);
            yield return null;
        }

        transform.position = destinationPoint;
        agent.enabled = true;
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
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, goToLayerSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destinationPoint;
        agent.enabled = true;
        hasReachSurface = true;
    }

    public IEnumerator SpawnCoroutine()
    {
        enemyAnimator.SetTrigger("Spawn");

        yield return null;
    }
}
