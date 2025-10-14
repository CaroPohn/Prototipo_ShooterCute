using UnityEngine;

public class ElectricAbility : MonoBehaviour
{
    public float desiredDistance = 10f; 
    public float angleDegrees = 45f;    
    public float projectileGravity = -9.81f;

    public GameObject player;
    private WeaponChanger weaponChangerScript;

    private Collider zapCollider;

    public bool hasAbilityBeenUsed;

    private InputReader inputReader;

    private void Awake()
    {
        inputReader = GameObject.FindGameObjectWithTag("InputReader").GetComponent<InputReader>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        weaponChangerScript = player.GetComponent<WeaponChanger>();
        EnsureComponents();
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptLaunchProjectile;

        hasAbilityBeenUsed = false;

        EnsureComponents();

        zapCollider.enabled = false;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptLaunchProjectile;
    }

    private void EnsureComponents()
    {
        if (zapCollider == null)
        {
            zapCollider = GetComponent<Collider>();
        }
    }

    private void AttemptLaunchProjectile()
    {
        if (!hasAbilityBeenUsed) 
        { 
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        hasAbilityBeenUsed = true;
        zapCollider.enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;

        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float v = Mathf.Sqrt((desiredDistance * -projectileGravity) / Mathf.Sin(2 * angleRad));

        Vector3 direction = Quaternion.AngleAxis(-angleDegrees, transform.parent.right) * transform.parent.forward;

        rb.useGravity = true;
        rb.linearVelocity = direction * v;

        transform.parent = null;

        weaponChangerScript.FillAbilityImage.fillAmount = 0;
        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;

        weaponChangerScript.ChangeWeapon();
    }
}