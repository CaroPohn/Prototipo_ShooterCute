using UnityEngine;

public class RotGuns : MonoBehaviour
{
    [SerializeField] Transform cameraOrientation;

    private void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(cameraOrientation.eulerAngles.x, cameraOrientation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
    }
}
