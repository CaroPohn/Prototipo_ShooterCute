using UnityEngine;
using UnityEngine.AI;

public class ImpactIceArea : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private int explosionRadius = 5;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        DamageToEnemys();
    }

    private void DamageToEnemys()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            NavMeshAgent agent = hitCollider.GetComponentInParent<NavMeshAgent>();

            if (agent != null && agent.gameObject.tag == "Enemy")
            {
                agent.speed = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}