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

    [SerializeField] private Electric_Gun_VFX electric_Gun_VFX_Script;

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

        if (Time.time - lastShootTime < timeBetweenShots)
            return;

        electric_Gun_VFX_Script.Charge();
    }

    private void ReleaseShoot()
    { 
        if (Time.time - lastShootTime < timeBetweenShots)
            return;

        isHoldingShoot = false;

        electric_Gun_VFX_Script.Release();

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
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 shootPoint = screenCenter;

        Ray ray = playerCamera.ScreenPointToRay(shootPoint);

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);

        if (Physics.Raycast(ray, out rayHit, range))
        {
            HealthSystem health = rayHit.collider.GetComponentInParent<HealthSystem>();
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
