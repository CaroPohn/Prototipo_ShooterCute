using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    Vector2 mouse;

    [SerializeField] private InputReader inputReader;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputReader.OnMoveCamera += AttemptCameraMove;
    }

    private void OnDisable()
    {
        inputReader.OnMoveCamera -= AttemptCameraMove;
    }

    private void Update()
    {
        yRotation += mouse.x;
        xRotation -= mouse.y;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void AttemptCameraMove(Vector2 dir)
    {
        mouse.x = dir.x * Time.deltaTime * sensX;
        mouse.y = dir.y * Time.deltaTime * sensY;
    }
}
