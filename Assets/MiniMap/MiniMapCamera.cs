using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position;

        newPosition.y = transform.position.y;

        transform.position = newPosition;

        //Quaternion newRotation = playerTransform.rotation;

        //newRotation.z = transform.rotation.z;

        //transform.rotation = newRotation;
    }
}
