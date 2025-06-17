using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float damage;
    private Vector3 moveDirection;
    private Rigidbody projectileRB;
    public float speed;

    private float lifeTime = 5.0f;

    private int counter;

    private void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
        projectileRB.useGravity = false;
        projectileRB.collisionDetectionMode = CollisionDetectionMode.Continuous;

        counter = 0;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        projectileRB.AddForce(moveDirection * speed, ForceMode.Impulse);
    }

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Player") && counter <= 1)
        {
            counter++;

            Destroy(gameObject);

            PlayerHealthSystem healthSystem = collision.transform.GetComponent<PlayerHealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage);
                healthSystem.SetDamageType(PlayerHealthSystem.DamageType.Projectile);
            }   
        }
    }
}

