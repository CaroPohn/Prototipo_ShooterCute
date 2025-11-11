using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class FireGun : Gun
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Animator bombAnimator;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private LevelController levelController;

    [SerializeField] private BombAnimationHandler bombAnimationHandler;
    
    public float damage;

    private bool canPlayerShoot;

    [SerializeField] InputReader inputReader;

    private void Start()
    {
        AkUnitySoundEngine.SetSwitch("Player_Shoot_Type", "Basic", gameObject);
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptShoot;
        bombAnimationHandler.OnShotEnd += ChangeShotBool;
        WeaponChanger.OnAbilitySelected += TriggerCancelAnimation;
    
        canPlayerShoot = true;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptShoot;
        bombAnimationHandler.OnShotEnd -= ChangeShotBool;
        WeaponChanger.OnAbilitySelected -= TriggerCancelAnimation;
    }

    public void ChangeShotBool()
    {
        canPlayerShoot = true;
    }

    private void TriggerCancelAnimation()
    {
        armsAnimator.SetTrigger("CancelAnimation");
        armsAnimator.Play("IDLE");
    }

    public void AttemptShoot()
    {
        if (canPlayerShoot)
        {
            if (!levelController.isGamePaused)
            {
                Shoot();
            }
        }
    }

    public override void Shoot()
    {
        AkUnitySoundEngine.PostEvent("Player_Shoot", gameObject);

        canPlayerShoot = false;

        Instantiate(muzzleFlash, shootPoint);

        bombAnimator.SetTrigger("Shot");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        armsAnimator.SetTrigger("Shoot");

        Vector3 direction = playerCamera.transform.forward;

        PlayerProjectile projScript = projectile.GetComponent<PlayerProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
        }
    }
}
