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

    [SerializeField] private LevelController levelController;

    [SerializeField] private Animator zapAnimator;

    private int totalDamage;

    private VisualEffect activeMuzzleEffect;

    private uint chargeEventPlayingId;

    private void Start()
    {
        AkUnitySoundEngine.SetSwitch("Player_Shoot_Type", "Electric", gameObject);
    }

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
        if (!levelController.isGamePaused)
        {
            if (isHoldingShoot)
                return;

            zapAnimator.SetBool("Charging", true);

            isHoldingShoot = true;
            shootHoldTime = 0f;

            electric_Gun_VFX_Script.Charge();

            armsAnimator.SetBool(armsAnimationName, true);

            chargeEventPlayingId = AkUnitySoundEngine.PostEvent("Player_ShootCharge_Electric", gameObject);
        }
    }

    private void ReleaseShoot()
    {
        if (!levelController.isGamePaused)
        {
            if (!isHoldingShoot)
                return;

            isHoldingShoot = false;
            zapAnimator.SetBool("Charging", false);
            armsAnimator.SetBool(armsAnimationName, false);

            if (chargeEventPlayingId != 0)
            {
                AkUnitySoundEngine.ExecuteActionOnEvent("Player_ShootCharge_Electric", AkActionOnEventType.AkActionOnEventType_Stop, gameObject);
                chargeEventPlayingId = 0;
            }

            if (Time.time - lastShootTime < timeBetweenShots)
            {
                electric_Gun_VFX_Script.Release(0, 0, shootPivot);
                shootHoldTime = 0f;
                return;
            }

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

            electric_Gun_VFX_Script.Release(hitDistance, 0.5f, shootPivot);
            AkUnitySoundEngine.PostEvent("Player_Shoot", gameObject);

            lastShootTime = Time.time;
            shootHoldTime = 0f;
        }
    }

    public override void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 shootPoint = screenCenter;

        Ray ray = playerCamera.ScreenPointToRay(shootPoint);

        RaycastHit[] hits = Physics.RaycastAll(ray, range);

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        int winLayer = LayerMask.NameToLayer("WinCollider");
        int playerLayer = LayerMask.NameToLayer("Player");

        bool hitSomething = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.layer == winLayer || hit.collider.gameObject.layer == playerLayer)
                continue;

            hitSomething = true;
            hitDistance = Vector3.Distance(hit.point, shootPoint);

            VisualEffect newHitVFX = Instantiate(hitPointEffect, hit.point, Quaternion.LookRotation(hit.normal));
            newHitVFX.Play();

            HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(totalDamage);
            }

            break;
        }

        if (!hitSomething)
        {
            Vector3 endPoint = ray.origin + ray.direction * range;
            hitDistance = range;

            VisualEffect newHitVFX = Instantiate(hitPointEffect, endPoint, Quaternion.LookRotation(-ray.direction));
            newHitVFX.Play();
        }

        if (activeMuzzleEffect != null)
        {
            activeMuzzleEffect.Stop();
            Destroy(activeMuzzleEffect.gameObject);
            activeMuzzleEffect = null;
        }
    }
}
