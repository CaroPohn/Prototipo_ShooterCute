using System.Collections;
using UnityEngine;

public class FreezeGun : Gun
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private GameObject hitEffect;

    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Animator iceAnimator;

    [SerializeField] private InputReader inputReader;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float distance = 100f;
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] private float timeBetweenBursts = 0.5f;

    private bool canPlayerShoot = true;

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptShoot;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptShoot;
    }

    public void AttemptShoot()
    {
        if (canPlayerShoot)
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        StartCoroutine(FireBurstRoutine());

        iceAnimator.SetTrigger("Shot");
        armsAnimator.SetTrigger("Spray_Shot");
    }

    private IEnumerator FireBurstRoutine()
    {
        canPlayerShoot = false;

        for (int i = 0; i < burstCount; i++)
        {
            ExecuteRaycastShot();

            yield return new WaitForSeconds(timeBetweenShots);
        }

        yield return new WaitForSeconds(timeBetweenBursts);

        canPlayerShoot = true;
    }

    private void ExecuteRaycastShot()
    {
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, shootPoint);
        }

        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Vector3 direction = playerCamera.transform.forward;

        PlayerProjectile projScript = projectile.GetComponent<PlayerProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
        }
    }
}
