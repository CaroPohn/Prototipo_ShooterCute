using UnityEngine;
using UnityEngine.VFX;

public class ZapGun : Gun
{
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots = 0f;

    private int damageLevel1 = 25;
    private int damageLevel2 = 40;
    private int damageLevel3 = 60;
    private int damageLevel4 = 120;

    private float shootHoldTime;
    private float lastShootTime;
    private bool isHoldingShoot;

    private float hitDistance;

    private string armsAnimationName = "Charging";

    [SerializeField] private Electric_Gun_VFX electric_Gun_VFX_Script;

    [SerializeField] private Animator armsAnimator;

    [SerializeField] private VisualEffect hitPointEffect;

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
        if (Time.time - lastShootTime >= timeBetweenShots)
        {
            isHoldingShoot = true;
            shootHoldTime = 0f;

            electric_Gun_VFX_Script.Charge();

            armsAnimator.SetBool(armsAnimationName, true);
        }
    }

    private void ReleaseShoot()
    {
        if (Time.time - lastShootTime < timeBetweenShots)
            return;

        isHoldingShoot = false;

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

        armsAnimator.SetBool(armsAnimationName, false);

        electric_Gun_VFX_Script.Release(hitDistance, 0.5f);

        lastShootTime = Time.time;
    }

    public override void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 shootPoint = screenCenter;

        Ray ray = playerCamera.ScreenPointToRay(shootPoint);

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);

        RaycastHit[] hits = Physics.RaycastAll(ray, range);

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        int winLayer = LayerMask.NameToLayer("WinCollider");
        int playerLayer = LayerMask.NameToLayer("Player");
        int worldLayer = LayerMask.NameToLayer("WorldCollider");

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.layer == winLayer || hit.collider.gameObject.layer == playerLayer || hit.collider.gameObject.layer == worldLayer)
                continue;

            hitDistance = Vector3.Distance(hit.point, shootPoint);

            Instantiate(hitPointEffect, hit.point, Quaternion.LookRotation(hit.normal));
            hitPointEffect.Play();

            HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(totalDamage);
            }

            break;
        }

        if (activeMuzzleEffect != null)
        {
            activeMuzzleEffect.Stop();
            Destroy(activeMuzzleEffect.gameObject);
            activeMuzzleEffect = null;
        }
    }
}
