using UnityEngine;
using UnityEngine.VFX;

public class ZapGun : Gun
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
    [SerializeField] Camera playerCamera;

    [SerializeField] InputReader inputReader;

    private int totalDamage;

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

        totalDamage = damageToDeal;
        Shoot();
        lastShootTime = Time.time;

        if (activeMuzzleEffect != null)
        {
            activeMuzzleEffect.Stop();
            Destroy(activeMuzzleEffect.gameObject); 
            activeMuzzleEffect = null;
        }
    }

    public override void Shoot()
    {
        Instantiate(rayEffect, shootPivot.position, shootPivot.rotation);

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector2 offset = Random.insideUnitCircle * spread;
        Vector3 shootPoint = screenCenter + new Vector3(offset.x, offset.y, 0f);

        Ray ray = playerCamera.ScreenPointToRay(shootPoint);

        if (Physics.Raycast(ray, out rayHit, range))
        {
            HealthSystem health = rayHit.collider.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(totalDamage);
            }
        }

        if (activeMuzzleEffect != null)
        {
            activeMuzzleEffect.Stop();
            Destroy(activeMuzzleEffect.gameObject);
            activeMuzzleEffect = null;
        }
    }
}
