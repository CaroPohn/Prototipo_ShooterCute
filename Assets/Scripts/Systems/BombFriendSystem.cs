using System.Collections;
using UnityEngine;

public class BombFriendSystem : MonoBehaviour
{
    public float runSpeed = 3f;
    public float maxDistance = 10f;
    public KeyCode activateKey = KeyCode.E;

    private bool isRunning = false;
    private Vector3 startPosition;
    private Transform player;
    [SerializeField] private Transform holdPosition;

    public float explosionForce;
    public float explosionRadius;
    public float damage;
    public float waitForExplosionTime;

    [SerializeField] private GameObject explosionVFXPrefab;

    private GameObject playerGO;
    private WeaponChanger weaponChangerScript;

    private InputReader inputReader;

    private void Awake()
    {
        inputReader = GameObject.FindGameObjectWithTag("InputReader").GetComponent<InputReader>();
    }

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        weaponChangerScript = playerGO.GetComponent<WeaponChanger>();

        player = transform.parent;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        Collider myCollider = GetComponent<Collider>();
        Collider playerCollider = player.GetComponent<Collider>();

        if (myCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(myCollider, playerCollider, true);
        }
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptDropAndRun;
    }

    private void OnDisable()
    {
         inputReader.OnShoot -= AttemptDropAndRun;
    }

    private void Update()
    {
        if (isRunning)
        {
            transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

            float distance = Vector3.Distance(startPosition, transform.position);
            if (distance >= maxDistance)
            {
                Explode();
            }
        }
    }

    private void AttemptDropAndRun()
    {
        if (!isRunning)
        {
            DropAndRun();
        }    
    }

    private void DropAndRun()
    {
        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;

        transform.parent = null;

        Vector3 dropPosition = player.position + player.forward * 1f;
        dropPosition.y = GetGroundY(dropPosition) + 0.1f;
        transform.position = dropPosition;

        transform.rotation = Quaternion.LookRotation(player.forward);

        startPosition = transform.position;
        isRunning = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }

    private float GetGroundY(Vector3 pos)
    {
        if (Physics.Raycast(pos + Vector3.up * 2f, Vector3.down, out RaycastHit hit, 5f))
        {
            return hit.point.y;
        }
        return pos.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isRunning)
        {
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private void Explode()
    {
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            Rigidbody hitRb = hitCollider.GetComponentInParent<Rigidbody>();

            if (hitRb != null && hitRb.tag == "Enemy")
            {
                HealthSystem enemyHealth = hitCollider.GetComponentInParent<HealthSystem>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage((int)damage);
                }
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(waitForExplosionTime);
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
