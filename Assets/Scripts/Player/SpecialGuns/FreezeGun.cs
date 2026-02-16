using System.Collections;
using UnityEngine;

public class FreezeGun : Gun
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera playerCamera;
    //[SerializeField] private GameObject muzzleFlash;
    //[SerializeField] private GameObject hitEffect;

    [SerializeField] private InputReader inputReader;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
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
        //if (muzzleFlash != null)
        //{
        //    Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation);
        //}

        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 rayDirection = playerCamera.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            //if (hitEffect != null)
            //{
            //    Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }
    }
}
