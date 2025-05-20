using UnityEngine;

public class ElectricAbility : MonoBehaviour
{
    public float desiredDistance = 10f; 
    public float angleDegrees = 45f;    
    public float projectileGravity = -9.81f;

    private GameObject player;
    private WeaponChanger weaponChangerScript;

    private bool hasAbilityBeenUsed;

    private InputReader inputReader;

    private void Awake()
    {
        inputReader = GameObject.FindGameObjectWithTag("InputReader").GetComponent<InputReader>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        weaponChangerScript = player.GetComponent<WeaponChanger>();

        hasAbilityBeenUsed = false;
    }

    private void OnEnable()
    {
        inputReader.OnShoot += AttemptLaunchProjectile;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptLaunchProjectile;
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

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;

        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float v = Mathf.Sqrt((desiredDistance * -projectileGravity) / Mathf.Sin(2 * angleRad));

        Vector3 direction = Quaternion.AngleAxis(-angleDegrees, transform.parent.right) * transform.parent.forward;

        rb.useGravity = true;
        rb.linearVelocity = direction * v;

        transform.parent = null;

        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;
    }
}