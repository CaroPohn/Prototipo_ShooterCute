using UnityEngine;

public class IceBurst : MonoBehaviour
{
    [SerializeField] GameObject impactAreaPrefab;
    private Transform parentTransform;

    private IceAbilityScript iceAbilityScript;

    private Rigidbody rb;

    private int count;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        iceAbilityScript = GetComponent<IceAbilityScript>();
    }

    private void OnEnable()
    {
        parentTransform = transform.parent;

        count = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (iceAbilityScript.hasAbilityBeenUsed && count < 1)
        {
            //AkUnitySoundEngine.PostEvent("Lumming_Ability_Electric_Impact", gameObject);

            count++;

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