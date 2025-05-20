using UnityEngine;
using UnityEngine.VFX;

public class FireGun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject muzzleFlash;
    
    public float damage;
    public float timeBetweenShots;

    private float timer;

    [SerializeField] InputReader inputReader;

    private void Start()
    {
        timer = 0.2f;
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptShoot;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptShoot;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void AttemptShoot()
    {
        if (timer >= timeBetweenShots)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        timer = 0;

        Instantiate(muzzleFlash, shootPoint);

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = playerCamera.transform.forward;

        PlayerProjectile projScript = projectile.GetComponent<PlayerProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
        }
    }
}
