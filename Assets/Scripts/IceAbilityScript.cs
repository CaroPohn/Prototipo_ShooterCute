using UnityEngine;

[RequireComponent(typeof(TrajectoryPreview))]
public class IceAbilityScript : MonoBehaviour
{
    public float minDistance = 5f;
    public float maxDistance = 15f;
    public float minAngle = 20f;
    public float maxAngle = 60f;

    public float projectileGravity = -9.81f;
    public float moveSpeed = 20f;

    public GameObject player;
    private WeaponChanger weaponChangerScript;
    private Collider iceCollider;
    private InputReader inputReader;

    public bool hasAbilityBeenUsed;
    private Vector3 moveDirection;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator armsAnimator;

    private TrajectoryPreview trajPrev;

    private void Awake()
    {
        inputReader = GameObject.FindGameObjectWithTag("InputReader").GetComponent<InputReader>();
        trajPrev = GetComponent<TrajectoryPreview>();
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

        SetSkinnedRenderersVisible(false);

        hasAbilityBeenUsed = false;
        iceCollider.enabled = false;

        trajPrev.TurnOn();
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= AttemptLaunchProjectile;
    }

    private void Update()
    {
        CalculateFall();
        trajPrev.ShowTrajectory(moveDirection);
    }

    public void SetSkinnedRenderersVisible(bool visible)
    {
        foreach (var skinnedRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            skinnedRenderer.enabled = visible;
        }
    }

    private void CalculateFall()
    {
        float cameraPitch = cameraTransform.eulerAngles.x;
        if (cameraPitch > 180f) cameraPitch -= 360f;

        float t = Mathf.InverseLerp(-45f, 45f, cameraPitch);
        float currentDistance = Mathf.Lerp(minDistance, maxDistance, t);
        float currentAngle = Mathf.Lerp(minAngle, maxAngle, t);

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        Vector3 launchDir = Quaternion.AngleAxis(-currentAngle, right) * forward;

        moveDirection = launchDir.normalized * moveSpeed;
    }

    private void EnsureComponents()
    {
        if (iceCollider == null)
        {
            iceCollider = GetComponent<Collider>();
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
        SetSkinnedRenderersVisible(true);
        armsAnimator.SetTrigger("Ability_Release");

        AkUnitySoundEngine.PostEvent("Lumming_Ability_Electric_Shoot", gameObject);
        AkUnitySoundEngine.PostEvent("UI_SkillReady", gameObject);

        hasAbilityBeenUsed = true;
        iceCollider.enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.linearVelocity = moveDirection;

        transform.parent = null;

        // Resetear arma
        weaponChangerScript.FillAbilityImage.fillAmount = 0;
        weaponChangerScript.timer = 0.0f;
        weaponChangerScript.weaponIndex = 1;

        weaponChangerScript.armsAnimator.SetBool("UsingAbility", false);
        weaponChangerScript.ChangeWeapon();

        trajPrev.TurnOff();
    }
}