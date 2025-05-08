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
    public bool allowHoldingButton;

    private bool isHoldingShoot;

    int bulletsLeft;
    int bulletsShot;

    bool readyToShoot;
    bool reloading;

    public RaycastHit rayHit;

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
        inputReader.OnShoot += StartShooting;
        inputReader.OnReload += AttemptReload;
        inputReader.OnHoldingShootCanceled += StopShooting;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= StartShooting;
        inputReader.OnReload -= AttemptReload;
        inputReader.OnHoldingShootCanceled -= StopShooting;
    }

    private void Update()
    {
        UpdateBulletsMagazine();
    }

    private void UpdateBulletsMagazine()
    {
        bulletsMagazine.text = (bulletsLeft + " / " + magazineSize);
    }

    private void StartShooting()
    {
        if (allowHoldingButton)
        {
            isHoldingShoot = true;
            AttemptShoot();
        }
        else
        {
            AttemptShoot();
        }
    }

    private void StopShooting()
    {
        isHoldingShoot = false;
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

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector2 offset = Random.insideUnitCircle * spread;
        Vector3 shootPoint = screenCenter + new Vector3(offset.x, offset.y, 0f);

        Ray ray = playerCamera.ScreenPointToRay(shootPoint);

        lineRenderer.enabled = true;

        if (Physics.Raycast(ray, out rayHit, range))
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
            lineRenderer.SetPosition(1, ray.origin + ray.direction * range);
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShoot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
        else if (allowHoldingButton && isHoldingShoot && bulletsLeft > 0)
        {
            Invoke(nameof(AttemptShoot), timeBetweenShooting);
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