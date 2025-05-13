using UnityEngine;

public class ElectricPulse : MonoBehaviour
{
    [SerializeField] GameObject impactAreaPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactAreaPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}