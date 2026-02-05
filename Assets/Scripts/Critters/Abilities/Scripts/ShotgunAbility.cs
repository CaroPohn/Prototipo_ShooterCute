using UnityEngine;

public class ShotgunAbility : MonoBehaviour
{
    [SerializeField] private float damagePerBullet;
    [SerializeField] private float distance;
    [SerializeField] private float spread;
    [SerializeField] private int raysPerShot;

    [SerializeField] private GameObject lineRenderer;
    [SerializeField] private Transform shootPoint;

    [SerializeField] InputReader inputReader;

    private GameObject playerGO;
    private WeaponChanger weaponChangerScript;

    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        weaponChangerScript = playerGO.GetComponent<WeaponChanger>();
    }

    private void OnEnable()
    {
        inputReader.OnShoot += ShootAbility;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= ShootAbility;
    }

    public void ShootAbility()
    {
        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;
        weaponChangerScript.FillAbilityImage.fillAmount = 0;

        weaponChangerScript.armsAnimator.SetBool("UsingAbility", false);
        weaponChangerScript.ChangeWeapon();

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

                TestLineRenderer(hit.point);
            }
            else
            {
                TestLineRenderer(shootPoint.position + shotDir * distance);
            }
        }

        gameObject.SetActive(false);
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
