using UnityEngine;

public class RotatingElement : MonoBehaviour
{
    [SerializeField] float autoRotateSpeed = 20f;
    [SerializeField] float dragRotateSpeed = 5f;

    private bool isDragging = false;
    private float lastMouseX;

    void Update()
    {
        HandleMouseDrag();

        if (!isDragging)
        {
            transform.Rotate(Vector3.up * autoRotateSpeed * Time.deltaTime, Space.World);
        }
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float deltaX = Input.mousePosition.x - lastMouseX;

            transform.Rotate(Vector3.up, -deltaX * dragRotateSpeed, Space.World);

            lastMouseX = Input.mousePosition.x;
        }
    }
}