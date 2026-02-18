using System.Collections;
using UnityEngine;

public class FreezeGun : Gun
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject muzzleFlash;
    //[SerializeField] private GameObject hitEffect;

    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Animator iceAnimator;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject lineRenderer;

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

        Vector3 rayDirection = playerCamera.transform.forward;

        RaycastHit hit;

        if ((Physics.Raycast(shootPoint.position, rayDirection, out hit, distance)))
        {
            HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            TestLineRenderer(hit.point);
        }
        else
        {
            TestLineRenderer(shootPoint.position + rayDirection * distance);
        }
    }

    private void TestLineRenderer(Vector3 end)
    {
        LineRenderer lR = Instantiate(lineRenderer).GetComponent<LineRenderer>();

        lR.SetPositions(new Vector3[2] { shootPoint.position, end });
    }
}
