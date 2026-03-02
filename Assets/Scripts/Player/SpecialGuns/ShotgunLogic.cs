using UnityEngine;

public class ShotgunLogic : Gun
{
    [SerializeField] private float damagePerBullet;
    [SerializeField] private float distance;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float spread;
    [SerializeField] private int raysPerShot;

    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Animator jhonnyAnimator;

    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] InputReader inputReader;

    [SerializeField] private Camera playerCamera;

    private void OnEnable()
    {
        inputReader.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= Shoot;
    }

    public override void Shoot()
    {
        for (int i = 0; i < raysPerShot; i++) 
        {
            Vector3 shotDir = GetShotSpread();

            GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            PlayerProjectile projScript = projectile.GetComponent<PlayerProjectile>();
            if (projScript != null)
            {
                projScript.SetDamage(damagePerBullet);
                projScript.SetDirection(shotDir);
            }
        }

        Instantiate(muzzleFlash, shootPoint);

        armsAnimator.SetTrigger("Shoot");
        jhonnyAnimator.SetTrigger("Shot");
    }

    private Vector3 GetShotSpread()
    {
        Vector3 shootDir = shootPoint.position + playerCamera.transform.forward * distance;

        shootDir = new Vector3(
            shootDir.x + Random.Range(-spread, spread),
            shootDir.y + Random.Range(-spread, spread),
            shootDir.z + Random.Range(-spread, spread)
            );

        Vector3 direction = shootDir - shootPoint.position;
        return direction;
    }
}
