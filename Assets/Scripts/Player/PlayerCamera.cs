using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float sensX;
    private float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    Vector2 mouse;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private SettingsManager settingsManager;

    private void Start()
    {
        sensX = 20.0f;
        sensY = 20.0f;
    }

    private void OnEnable()
    {
        inputReader.OnMoveCamera += AttemptCameraMove;
        settingsManager.OnSensChange += ChangeSens;
    }

    private void OnDisable()
    {
        inputReader.OnMoveCamera -= AttemptCameraMove;
        settingsManager.OnSensChange -= ChangeSens;
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

    private void ChangeSens()
    {
        sensX = settingsManager.GetSensitivity();
        sensY = settingsManager.GetSensitivity();
    }
}
