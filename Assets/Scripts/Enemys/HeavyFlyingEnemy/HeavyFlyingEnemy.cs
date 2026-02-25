using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class HeavyFlyingEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float flyingHeight;

    [SerializeField] private float stopDistance;

    private float distanceToPlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent.baseOffset = flyingHeight;
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        MoveToTarget();
        StopAgent();

        distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    private void MoveToTarget()
    {
        if (target != null && distanceToPlayer >= stopDistance) 
        { 
            agent.isStopped = false;
            agent.destination = target.position;
        }
    }

    private void StopAgent()
    {
        if (distanceToPlayer <= stopDistance) 
        {
            agent.isStopped = true;
        }  
    }
}
