using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool isBobEnabled = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float frequency = 10.0f;

    [Header("Main Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraHolderTransform;

    [Header("UI Canvas")]
    [SerializeField] private RectTransform uiContainer;
    [SerializeField] private float uiMultiplier = 50f;

    private Vector3 startPos;
    private Vector2 startUIPos;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        startPos = cameraTransform.localPosition;

        if (uiContainer != null)
        {
            startUIPos = uiContainer.anchoredPosition;
        }
    }

    private void Update()
    {
        if (isBobEnabled)
        {
            CheckMotion();
            cameraTransform.LookAt(FocusTarget());
        }
    }

    private void CheckMotion()
    {
        if (playerMovement.isMoving)
        {
            PlayMotion(FootStepMotion());
        }
        else
        {
            ResetPosition();
        }
    }

    private void PlayMotion(Vector3 motion)
    {
        cameraTransform.localPosition = startPos + motion;

        if (uiContainer != null)
        {
            Vector2 uiMotion = new Vector2(motion.x, motion.y) * uiMultiplier;
            uiContainer.anchoredPosition = startUIPos + uiMotion;
        }
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private void ResetPosition()
    {
        if (cameraTransform.localPosition != startPos)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, startPos, 5f * Time.deltaTime);
        }

        if (uiContainer != null && uiContainer.anchoredPosition != startUIPos)
        {
            uiContainer.anchoredPosition = Vector2.Lerp(uiContainer.anchoredPosition, startUIPos, 5f * Time.deltaTime);
        }
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolderTransform.localPosition.y, transform.position.z);
        pos += cameraHolderTransform.forward * 15.0f;
        return pos;
    }
}