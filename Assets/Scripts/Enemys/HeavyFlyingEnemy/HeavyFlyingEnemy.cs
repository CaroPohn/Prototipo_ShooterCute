using UnityEngine;
using UnityEngine.AI;

public class HeavyFlyingEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float flyingHeight;

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
    }

    private void MoveToTarget()
    {
        if (target != null) 
        { 
            agent.destination = target.position;
        }
    }
}
