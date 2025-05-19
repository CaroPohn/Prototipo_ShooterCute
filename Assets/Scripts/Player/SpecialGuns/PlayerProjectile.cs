using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float damage;
    private Vector3 moveDirection;
    private Rigidbody projectileRB;
    public float speed;
    public float fallGravity = 9.81f;

    private float lifeTime = 5.0f;

    private int counter;

    [System.Obsolete]
    private void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
        projectileRB.useGravity = false;
        projectileRB.collisionDetectionMode = CollisionDetectionMode.Continuous;

        projectileRB.velocity = moveDirection.normalized * speed;

        Destroy(gameObject, lifeTime);
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        projectileRB.velocity += Vector3.down * fallGravity * Time.fixedDeltaTime;
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
        if (!collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        counter++;

        if (collision.transform.CompareTag("Enemy") && counter <= 1)
        {
            Destroy(gameObject);

            HealthSystem healthSystem = collision.transform.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage);
            }
        }
    }
}

