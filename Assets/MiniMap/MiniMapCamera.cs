using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private RectTransform radarUITransform;

    private void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position;

        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }

    private void Update()
    {
        //Quaternion newRotation = new Quaternion(0, 0, 0, 0);

        //newRotation.z = playerTransform.rotation.y;

        //radarUITransform.rotation = new Quaternion(radarUITransform.rotation.x, radarUITransform.rotation.y, newRotation.z, radarUITransform.rotation.w);

        float zRotation = playerTransform.eulerAngles.z;

        radarUITransform.localEulerAngles = new Vector3(0f, 0f, zRotation);
    }
}
