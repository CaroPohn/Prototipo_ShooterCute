using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private Vector3 moveDirection;
    private Rigidbody projectileRB;
    public float speed;

    private void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
        projectileRB.useGravity = false;
        projectileRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);

            HealthSystem playerHealth = collision.transform.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }   
        }
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}

