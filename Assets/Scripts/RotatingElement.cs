using UnityEngine;

public class RotatingElement : MonoBehaviour
{
    [SerializeField] float rotatingSpeed = 1;
    
    void Update()
    {
        transform.Rotate(new Vector3 (0, rotatingSpeed * Time.deltaTime, 0));
    }
}
