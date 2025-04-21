using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float speed = 2f;
    public float rotationSpeed = 5f;
    public float reachDistance = 0.2f;

    [SerializeField] Transform player;

    public float followDistance;
    public float followSpeed;

    private int currentPointIndex = 0;

    public void MoveToNextPoint()
    {
        Transform targetPoint = patrolPoints[currentPointIndex];

        Vector3 direction = (targetPoint.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) <= reachDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        }
    }

    public bool IsPlayerOnRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followDistance)
        {
            return true;
        }

        return false;
    }

    public void FollowPlayer()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.position += moveDirection * followSpeed * Time.deltaTime;
    }

    public void SetLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints == null || patrolPoints.Count == 0) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);

            if (i < patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}