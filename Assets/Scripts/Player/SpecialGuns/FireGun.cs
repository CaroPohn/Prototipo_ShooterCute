using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class FireGun : Gun
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] private Animator armsAnimation;
    [SerializeField] private Animator bombAnimator;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private LevelController levelController;
    
    public float damage;
    public float timeBetweenShots;

    private float timer;

    [SerializeField] InputReader inputReader;

    private void Start()
    {
        timer = 0.2f;

        AkUnitySoundEngine.SetSwitch("Player_Shoot_Type", "Basic", gameObject);
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptShoot;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptShoot;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void AttemptShoot()
    {
        if (timer >= timeBetweenShots)
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

        timer = 0;

        Instantiate(muzzleFlash, shootPoint);

        bombAnimator.SetTrigger("Shot");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        armsAnimation.SetTrigger("Shoot");

        Vector3 direction = playerCamera.transform.forward;

        PlayerProjectile projScript = projectile.GetComponent<PlayerProjectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
        }
    }
}
