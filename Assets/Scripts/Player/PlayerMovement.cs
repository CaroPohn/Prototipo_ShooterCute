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

    public float playerHeight;
    private bool IsPlayerOnGround;

    public Transform orientation;
    public Transform cameraOrientation;
    private Vector2 inputDir;
    private Vector3 moveDirection;
    private Rigidbody rb;

    [SerializeField] InputReader inputReader;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        rb.useGravity = false;
    }

    private void OnEnable()
    {
        inputReader.OnJump += AttemptJump;
        inputReader.OnMove += AttemptMove;

    }

    private void OnDisable()
    {
        inputReader.OnJump -= AttemptJump;
        inputReader.OnMove -= AttemptMove;
    }

    private void Update()
    {
        IsPlayerOnGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        Quaternion targetRotation = Quaternion.Euler(0, cameraOrientation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
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

    private void AttemptMove(Vector2 value)
    {
        inputDir = value;
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            return hit.normal;
        }

        return Vector3.up;
    }

    private void MovePlayer()
    {
        moveDirection = cameraOrientation.forward * inputDir.y + cameraOrientation.right * inputDir.x;

        Vector3 groundNormal = GetGroundNormal();
        Vector3 adjustedDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

        float multiplier = (IsPlayerOnGround) ? 10f : 10f * airMultiplier;
        rb.AddForce(adjustedDirection * moveSpeed * multiplier, ForceMode.Force);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
