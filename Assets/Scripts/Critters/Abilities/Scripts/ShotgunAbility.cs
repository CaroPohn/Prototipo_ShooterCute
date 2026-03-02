using System.Diagnostics;
using UnityEngine;

public class ShotgunAbility : MonoBehaviour
{
    [SerializeField] private float damagePerBullet;
    [SerializeField] private float distance;
    [SerializeField] private float spread;
    [SerializeField] private int raysPerShot;
    [SerializeField] private float recoilForce;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] InputReader inputReader;

    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Animator jhonnyAnimator;

    private GameObject playerGO;
    private Rigidbody playerRB;
    private WeaponChanger weaponChangerScript;

    private Camera playerCamera;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerCamera = playerGO.GetComponentInChildren<Camera>();
        playerRB = playerGO.GetComponent<Rigidbody>();
        weaponChangerScript = playerGO.GetComponent<WeaponChanger>();
    }

    private void OnEnable()
    {
        inputReader.OnShoot += ActivateAbility;
        ArmsAnimatorHandler.OnSqueezeJhonny += ShootAbility;
        ArmsAnimatorHandler.OnSqueezeJhonnyToGun += ChangeWeaponScriptValues;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= ActivateAbility;
        ArmsAnimatorHandler.OnSqueezeJhonny -= ShootAbility;
        ArmsAnimatorHandler.OnSqueezeJhonnyToGun -= ChangeWeaponScriptValues;
    }

    private void ActivateAbility()
    {
        armsAnimator.SetTrigger("Squeeze");
        jhonnyAnimator.SetTrigger("Squeeze");
    }

    private void ChangeWeaponScriptValues()
    {
        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;
        weaponChangerScript.FillAbilityImage.fillAmount = 0;

        weaponChangerScript.armsAnimator.SetBool("UsingAbility", false);
        weaponChangerScript.ChangeWeapon();

        gameObject.SetActive(false);
    }

    public void ShootAbility()
    {
        Vector3 recoilDirection = -playerCamera.transform.forward;

        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        for (int i = 0; i < raysPerShot; i++)
        {
            RaycastHit hit;
            Vector3 shotDir = GetShotSpread();

            playerRB.AddForce(recoilDirection * recoilForce, ForceMode.Impulse);

            if ((Physics.Raycast(shootPoint.position, GetShotSpread(), out hit, distance)))
            {
                HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();

                if (health != null)
                {
                    health.TakeDamage(damagePerBullet);
                }
            }
        }
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
}
