using System.Collections.Generic;
using UnityEngine;

public class HeavyFExplotionManager : MonoBehaviour
{
    [SerializeField] private GameObject explotionVFX;

    [SerializeField] private float explotionRadius;
    [SerializeField] private float damage;

    [SerializeField] private LayerMask damageableLayers;

    private void Start()
    {
        Explotion();
    }

    private void Explotion()
    {
        if (explotionVFX != null)
        {
            Instantiate(explotionVFX, transform.position, transform.rotation);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explotionRadius, damageableLayers);

        HashSet<GameObject> damagedObjects = new HashSet<GameObject>();

        foreach (Collider hit in hitColliders)
        {
            GameObject root = hit.transform.root.gameObject;

            if (damagedObjects.Contains(root))
            {
                continue;
            }

            bool hasDamaged = false;

            HealthSystem health = hit.GetComponent<HealthSystem>();
            PlayerHealthSystem playerHealth = hit.GetComponent<PlayerHealthSystem>();

            if (health != null)
            {
                health.TakeDamage(damage);
                hasDamaged = true;
            }

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                hasDamaged = true;
            }

            if (hasDamaged) 
            {
                damagedObjects.Add(root);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
