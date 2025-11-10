using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float groundDrag;
    public float airDrag;
    public Vector3 customGravity;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    private bool isMoving;

    [SerializeField] private Animator armsAnimator;

    public float playerHeight;
    private bool IsPlayerOnGround;

    public Transform orientation;
    public Transform cameraOrientation;
    private Vector2 inputDir;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private bool hasStepSoundReproduce;
    private float stepTimer;

    private float animVelocity;

    public float stepTime;

    [SerializeField] InputReader inputReader;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        rb.useGravity = false;

        transform.rotation = Quaternion.Euler(0, orientation.eulerAngles.y, 0);
        hasStepSoundReproduce = false;
    }

    private void OnEnable()
    {
        inputReader.OnJump += AttemptJump;
        inputReader.OnMove += AttemptMove;

        EggInteraction.OnGrabbingEgg += GrabEggAnimation;
    }

    private void OnDisable()
    {
        inputReader.OnJump -= AttemptJump;
        inputReader.OnMove -= AttemptMove;

        EggInteraction.OnGrabbingEgg -= GrabEggAnimation;
    }

    private void Update()
    {
        IsPlayerOnGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        stepTimer += Time.deltaTime;

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        isMoving = horizontalVelocity.magnitude > 5f;

        ChangeStepSoundBool();

        FootstepAudioSwitch();

        WalkAnimationHandler();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        rb.AddForce(customGravity * Time.fixedDeltaTime, ForceMode.Acceleration);

        if (IsPlayerOnGround)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = airDrag;
        }
    }

    private void WalkAnimationHandler()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float speed = horizontalVelocity.magnitude;

        float minSpeed = 0f;
        float maxSpeed = 8f;

        float targetAnimVelocity = Mathf.InverseLerp(minSpeed, maxSpeed, speed);

        animVelocity = Mathf.Lerp(animVelocity, targetAnimVelocity, Time.deltaTime * 10f);

        armsAnimator.SetFloat("Speed", animVelocity);
    }

    private void AttemptMove(Vector2 value)
    {
        inputDir = value;
    }

    private void FootstepAudioSwitch()
    {
        string groundLayer = GetGroundLayer();

        switch (groundLayer)
        {
            case "Default":
                AkUnitySoundEngine.SetSwitch("Footstep_Surface", "Stone", gameObject);
                break;

            case "IgnoreNavMesh":
                AkUnitySoundEngine.SetSwitch("Footstep_Surface", "Stone", gameObject);
                break;

            case "Lava":
                AkUnitySoundEngine.SetSwitch("Footstep_Surface", "Lava", gameObject);
                break;

            case "NavLava":
                AkUnitySoundEngine.SetSwitch("Footstep_Surface", "Lava", gameObject);
                break;
        }
    }

    private string GetGroundLayer()
    {
        string groundLayer;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.2f))
        {
            int layerIndex = hit.collider.gameObject.layer;
            groundLayer = LayerMask.LayerToName(layerIndex);

            return groundLayer;
        }

        return null;
    }

    private void ChangeStepSoundBool()
    {
        if (stepTimer > stepTime)
        {
            stepTimer = 0;
            hasStepSoundReproduce = !hasStepSoundReproduce;

            if (hasStepSoundReproduce && isMoving && IsPlayerOnGround)
            {
                AkUnitySoundEngine.PostEvent("Player_Footstep", gameObject);
            }
        }
    }

    //private void WalkAnimationHandler()
    //{
    //    armsAnimator.SetFloat("Speed", 1);
    //}

    private void GrabEggAnimation()
    {
        armsAnimator.SetTrigger("Grab");
    }

    private void AttemptJump()
    {
        if (readyToJump && (IsPlayerOnGround))
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private Vector3 GetGroundNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f + 0.2f))
        {
            return hit.normal;
        }

        return Vector3.up;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * inputDir.y + orientation.right * inputDir.x;

        Vector3 groundNormal = GetGroundNormal();
        Vector3 adjustedDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

        float multiplier = (IsPlayerOnGround) ? 10f : 10f * airMultiplier;
        rb.AddForce(adjustedDirection * moveSpeed * multiplier, ForceMode.Force);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        AkUnitySoundEngine.PostEvent("Player_Jump", gameObject);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
