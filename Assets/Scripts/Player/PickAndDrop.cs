using System;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    [SerializeField] private Transform holdPosition;
    private GameObject heldEgg;

    public event Action<GameObject> OnEggGrabbed;
    public event Action<GameObject> OnEggDropped;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldEgg == null)
            {
                TryPickUp();
            }
            else
            {
                PlaceEgg();
            }
        }

        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * 5f, Color.red);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 5f))
        {
            Debug.Log("Apuntando a: " + hit.collider.gameObject.name);
        }
    }

    private void TryPickUp()
    {
        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 5f))
        {
            if (hit.collider.CompareTag("Egg"))
            {
                heldEgg = hit.collider.gameObject;

                heldEgg.transform.SetParent(holdPosition);
                heldEgg.transform.localPosition = Vector3.zero;
                heldEgg.transform.localRotation = Quaternion.identity;

                OnEggGrabbed?.Invoke(heldEgg);

                Rigidbody rb = heldEgg.GetComponent<Rigidbody>();

                rb.isKinematic = true;

                Collider eggCollider = heldEgg.GetComponent<Collider>();
                Collider playerCollider = GetComponent<Collider>();

                if (playerCollider != null && eggCollider != null)
                {
                    Physics.IgnoreCollision(eggCollider, playerCollider, true);
                }
            }
        }
    }

    private void PlaceEgg()
    {
        if (heldEgg != null)
        {
            RaycastHit hit;
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 rayDirection = Camera.main.transform.forward;

            int layerMask = ~LayerMask.GetMask("Egg");

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 7f, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.CompareTag("Floor"))
                {
                    heldEgg.transform.SetParent(null);

                    float eggHeight = heldEgg.GetComponent<Collider>().bounds.extents.y;

                    heldEgg.transform.position = hit.point + Vector3.up * eggHeight;
                    heldEgg.transform.rotation = Quaternion.identity;

                    Rigidbody rb = heldEgg.GetComponent<Rigidbody>();
                    rb.isKinematic = true;

                    OnEggDropped?.Invoke(heldEgg);
                    heldEgg = null;
                }
                else
                {
                    Debug.Log("No estás apuntando al piso.");
                }
            }
            else
            {
                Debug.Log("No hay nada en el raycast.");
            }
        }
    }
}

