using UnityEngine;
using UnityEngine.VFX;

public class PlayerProjectile : MonoBehaviour
{
    private float damage;
    private Vector3 moveDirection;
    private Rigidbody projectileRB;
    public float speed;
    public float fallGravity = 9.81f;

    [SerializeField] private GameObject effect;

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
        AkUnitySoundEngine.PostEvent("Projectile_Hit_BasicProjectile", gameObject);

        ContactPoint contact = collision.GetContact(0);
        Vector3 hitPoint = contact.point;
        Vector3 hitNormal = contact.normal;

        GameObject vfx = Instantiate(effect, hitPoint, Quaternion.LookRotation(hitNormal));

        VisualEffect visual = vfx.GetComponent<VisualEffect>();
        if (visual != null)
        {
            visual.Play();
        }

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

