using UnityEditor.Timeline;
using UnityEngine;

public class ElectricPulse : MonoBehaviour
{
    [SerializeField] GameObject impactAreaPrefab;
    private Transform parentTransform;

    private ElectricAbility electricAbilityScript;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        electricAbilityScript = GetComponent<ElectricAbility>();
    }

    private void OnEnable()
    {
        parentTransform = transform.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (electricAbilityScript.hasAbilityBeenUsed)
        {
            Instantiate(impactAreaPrefab, transform.position, Quaternion.identity);

            transform.parent = parentTransform;
            transform.position = parentTransform.position;
            transform.rotation = parentTransform.rotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
            gameObject.SetActive(false);
        }     
    }
}