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

    [SerializeField] private GameObject lineRenderer;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject contactPoint;

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
            RaycastHit hit;
            Vector3 shotDir = GetShotSpread();

            if ((Physics.Raycast(shootPoint.position, GetShotSpread(), out hit, distance)))
            {
                HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();

                if (health != null) 
                {
                    health.TakeDamage(damagePerBullet);
                }

                Instantiate(contactPoint, hit.point, Quaternion.LookRotation(hit.normal));

                TestLineRenderer(hit.point);
            }
            else
            {
                TestLineRenderer(shootPoint.position + shotDir * distance);
            }
        }

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

    private void TestLineRenderer(Vector3 end)
    {
        LineRenderer lR = Instantiate(lineRenderer).GetComponent<LineRenderer>();

        lR.SetPositions(new Vector3[2] { shootPoint.position, end });
    }
}
