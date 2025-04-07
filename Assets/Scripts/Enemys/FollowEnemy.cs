using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] Transform player;

    public float followDistance;
    public float followSpeed;

    private void Update()
    {
        CheckIfPlayerIsOnRange();
    }

    private void CheckIfPlayerIsOnRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followDistance)
        {
            SetLookAt();
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.position += moveDirection * followSpeed * Time.deltaTime;
    }

    private void SetLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
