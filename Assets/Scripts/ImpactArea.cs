using UnityEngine;

public class ImpactArea : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private int explosionRadius = 5;
    [SerializeField] private float damage;

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
            Rigidbody hitRb = hitCollider.GetComponentInParent<Rigidbody>();

            if (hitRb != null && hitRb.tag == "Enemy")
            {
                HealthSystem enemyHealth = hitCollider.GetComponentInParent<HealthSystem>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}