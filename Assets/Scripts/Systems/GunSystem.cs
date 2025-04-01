using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;

    int bulletsLeft;
    int bulletsShot;

    bool readyToShoot;
    bool reloading;

    public RaycastHit rayHit;

    Vector3 direction;

    [SerializeField] public TextMeshProUGUI bulletsMagazine;
    [SerializeField] GameObject shootPivot;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Camera playerCamera;

    [SerializeField] InputReader inputReader;

    private void Start()
    {
        readyToShoot = true;
        reloading = false;
        bulletsLeft = magazineSize;
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptShoot;
        inputReader.OnReload += AttemptReload;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptShoot;
        inputReader.OnReload -= AttemptReload;
    }

    private void Update()
    {
        UpdateBulletsMagazine();
    }

    private void UpdateBulletsMagazine()
    {
        bulletsMagazine.text = (bulletsLeft + " / " + magazineSize);
    }

    private void AttemptShoot()
    {
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void AttemptReload()
    {
        Reload();
    }

    public void Shoot()
    {
        readyToShoot = false;

        lineRenderer.enabled = false;
        lineRenderer.transform.position = shootPivot.transform.position;
        lineRenderer.SetPosition(0, shootPivot.transform.position);

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 spreadDirection = shootPivot.transform.forward + new Vector3(x, y, 0);

        lineRenderer.enabled = true;

        if (Physics.Raycast(shootPivot.transform.position, spreadDirection, out rayHit, range))
        {           
            lineRenderer.SetPosition(1, rayHit.point);

            HealthSystem health = rayHit.collider.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, playerCamera.transform.position + (playerCamera.transform.forward * range));
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShoot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShoot()
    {
        readyToShoot = true;
        lineRenderer.enabled = false;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}