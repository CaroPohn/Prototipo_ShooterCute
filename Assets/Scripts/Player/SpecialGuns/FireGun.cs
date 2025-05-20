using UnityEngine;
using UnityEngine.VFX;

public class FireGun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject muzzleFlash;
    
    public float damage;
    public float timeBetweenShots = 0.5f;

    [SerializeField] InputReader inputReader;

    private void OnEnable()
    {
        inputReader.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= Shoot;
    }

    public void Shoot()
    {
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
