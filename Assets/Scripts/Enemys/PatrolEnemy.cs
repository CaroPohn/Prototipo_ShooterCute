using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Follow")]
    private Transform player;

    public float followDistance;

    [Header("Shoot")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;

    public float shootCoolDown;
    public float damage;
    public float shootTimer;

    private NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    public bool IsPlayerOnRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= followDistance;
    }

    public void FollowPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (agent != null)
        {
            agent.SetDestination(player.position);
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

    public void Shoot()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}