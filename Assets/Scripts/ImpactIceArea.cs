using UnityEngine;
using UnityEngine.AI;

public class ImpactIceArea : MonoBehaviour
{
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private float speedToApply = 0f;

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
            PatrolEnemy enemy = hitCollider.GetComponentInParent<PatrolEnemy>();
            MeleeEnemy meleeEnemy = hitCollider.GetComponentInParent<MeleeEnemy>();

            if (enemy != null && enemy.gameObject.tag == "Enemy" || meleeEnemy != null && meleeEnemy.gameObject.tag == "Enemy")
            {
                if (enemy != null)
                {
                    enemy.SlowEnemy(speedToApply, lifetime - 1);
                    enemy.frostTest = true;
                }                

                if (meleeEnemy != null)
                {
                    meleeEnemy.SlowEnemy(speedToApply, lifetime - 1);
                    meleeEnemy.frostTest = true;
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