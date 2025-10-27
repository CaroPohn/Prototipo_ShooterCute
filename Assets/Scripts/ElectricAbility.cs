using UnityEngine;

public class ElectricAbility : MonoBehaviour
{
    public float minDistance = 5f;
    public float maxDistance = 15f;
    public float minAngle = 20f;
    public float maxAngle = 60f;

    public float projectileGravity = -9.81f;
    public float moveSpeed = 20f;

    public GameObject player;
    private WeaponChanger weaponChangerScript;
    private Collider zapCollider;
    private InputReader inputReader;

    public bool hasAbilityBeenUsed;
    private Vector3 moveDirection;

    [SerializeField] private Transform cameraTransform;

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
        EnsureComponents();

        hasAbilityBeenUsed = false;
        zapCollider.enabled = false;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptLaunchProjectile;
    }

    private void Update()
    {
        CalculateFall();
    }

    private void CalculateFall()
    {
        float cameraPitch = cameraTransform.eulerAngles.x; // asumiendo que el hijo 0 es la cámara
        if (cameraPitch > 180f) cameraPitch -= 360f; // para tener rango [-180, 180]

        // Mapear inclinación de cámara a distancia y ángulo
        float t = Mathf.InverseLerp(-45f, 45f, cameraPitch); // -45° mirando arriba → 45° mirando abajo
        float currentDistance = Mathf.Lerp(minDistance, maxDistance, t);
        float currentAngle = Mathf.Lerp(minAngle, maxAngle, t);

        // Calcular la dirección de movimiento (trayectoria inicial)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        Vector3 launchDir = Quaternion.AngleAxis(-currentAngle, right) * forward;

        moveDirection = launchDir.normalized * moveSpeed;
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
        rb.useGravity = true;

        // Aplicar la dirección ya calculada
        rb.linearVelocity = moveDirection;

        // Desanclar del padre
        transform.parent = null;

        // Resetear arma
        weaponChangerScript.FillAbilityImage.fillAmount = 0;
        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;

        weaponChangerScript.ChangeWeapon();
    }
}