using UnityEditor.Timeline;
using UnityEngine;

public class ElectricPulse : MonoBehaviour
{
    [SerializeField] GameObject impactAreaPrefab;
    private Transform parentTransform;

    private Rigidbody rb;

    private void Start()
    {
        parentTransform = transform.parent;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactAreaPrefab, transform.position, Quaternion.identity);

        transform.parent = parentTransform;
        transform.position = parentTransform.position;
        transform.rotation = parentTransform.rotation;
        gameObject.SetActive(false);
    }
}