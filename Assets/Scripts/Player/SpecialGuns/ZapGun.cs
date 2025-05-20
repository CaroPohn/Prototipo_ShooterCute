using UnityEngine;
using UnityEngine.VFX;

public class ZapGun : MonoBehaviour
{
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots = 0.5f;

    private int damageLevel1 = 25;
    private int damageLevel2 = 40;
    private int damageLevel3 = 60;
    private int damageLevel4 = 120;

    private float shootHoldTime;
    private float lastShootTime;
    private bool isHoldingShoot;

    public RaycastHit rayHit;

    [SerializeField] private GameObject muzzleEffect;
    [SerializeField] private GameObject rayEffect;

    [SerializeField] Transform shootPivot;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Camera playerCamera;

    [SerializeField] InputReader inputReader;

    private VisualEffect activeMuzzleEffect;

    void OnEnable()
    {
        inputReader.OnShoot += StartHoldingShoot;
        inputReader.OnHoldingShootCanceled += ReleaseShoot;
    }

    void OnDisable()
    {
        inputReader.OnShoot -= StartHoldingShoot;
        inputReader.OnHoldingShootCanceled -= ReleaseShoot;
    }

    void Update()
    {
        if (isHoldingShoot)
        {
            shootHoldTime += Time.deltaTime;
        }
    }

    private void StartHoldingShoot()
    {
        isHoldingShoot = true;
        shootHoldTime = 0f;

        if (activeMuzzleEffect == null)
        {
            GameObject instance = Instantiate(muzzleEffect, shootPivot.position, shootPivot.rotation, shootPivot);
            activeMuzzleEffect = instance.GetComponent<VisualEffect>();
            activeMuzzleEffect.Play();
        }
    }

    private void ReleaseShoot()
    {
        isHoldingShoot = false;

        if (Time.time - lastShootTime < timeBetweenShots)
            return;

        int damageToDeal = damageLevel1;

        if (shootHoldTime >= 2f)
        {
            damageToDeal = damageLevel4;
        }
        else if (shootHoldTime >= 1f)
        {
            damageToDeal = damageLevel3;
        } 
        else if (shootHoldTime >= 0.5f)
        {
            damageToDeal = damageLevel2;
        }
            
        Shoot(damageToDeal);
        lastShootTime = Time.time;

        if (activeMuzzleEffect != null)
        {
            activeMuzzleEffect.Stop();
            Destroy(activeMuzzleEffect.gameObject); 
            activeMuzzleEffect = null;
        }
    }

    private void Shoot(int damage)
    {
        Instantiate(rayEffect, shootPivot.position, shootPivot.rotation);

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector2 offset = Random.insideUnitCircle * spread;
        Vector3 shootPoint = screenCenter + new Vector3(offset.x, offset.y, 0f);

        Ray ray = playerCamera.ScreenPointToRay(shootPoint);

        if (Physics.Raycast(ray, out rayHit, range))
        {
            lineRenderer.SetPosition(0, shootPivot.position);
            lineRenderer.SetPosition(1, rayHit.point);
            lineRenderer.enabled = true;
            Invoke(nameof(DisableLine), 0.05f);

            HealthSystem health = rayHit.collider.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, shootPivot.position);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * range);
            lineRenderer.enabled = true;
            Invoke(nameof(DisableLine), 0.05f);
        }

        if (activeMuzzleEffect != null)
        {
            activeMuzzleEffect.Stop();
            Destroy(activeMuzzleEffect.gameObject);
            activeMuzzleEffect = null;
        }
    }

    private void DisableLine()
    {
        lineRenderer.enabled = false;
    }
}
