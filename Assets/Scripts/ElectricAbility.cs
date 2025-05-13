using UnityEngine;

public class ElectricAbility : MonoBehaviour
{
    public float desiredDistance = 10f; 
    public float angleDegrees = 45f;    
    public float projectileGravity = -9.81f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;

        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float v = Mathf.Sqrt((desiredDistance * -projectileGravity) / Mathf.Sin(2 * angleRad));

        Vector3 direction = Quaternion.AngleAxis(-angleDegrees, transform.parent.right) * transform.parent.forward;

        rb.useGravity = true;
        rb.linearVelocity = direction * v;

        transform.parent = null;
    }
}