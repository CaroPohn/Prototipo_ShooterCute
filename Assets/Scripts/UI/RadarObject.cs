using System.Security.Cryptography;
using UnityEngine;

public class RadarObject : MonoBehaviour
{
    private Transform miniMapTransform;
    //private Vector3 initalPosition;
    private Vector3 initialLocalPosition;
    private float maxDistance = 44f;
    [SerializeField] private bool shouldUpdateTransform = true;
    [SerializeField] private bool shouldUpdateRotation = true;
    void Start()
    {
        //initalPosition = transform.position;
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (!miniMapTransform) miniMapTransform = GameObject.Find("MiniMapCamera").transform;
        if(shouldUpdateRotation)UpdateRotation();
        if(shouldUpdateTransform)UpdateTransform();
        
    }

    void UpdateRotation()
    {
        transform.rotation = miniMapTransform.rotation;
    }
    void UpdateTransform()
    {
        //Asi no importa la distancia en el eje Y, que no lo tiene en cuenta la camara al ser ortografica
        //Vector3 initalPositionSameY = new Vector3(initalPosition.x, 0, initalPosition.z);
        Vector3 initalPositionSameY = new Vector3(transform.parent.position.x, 0, transform.parent.position.z);
        Vector3 miniMapPositionSameY = new Vector3(miniMapTransform.position.x, 0, miniMapTransform.position.z);

        float distance = Vector3.Distance(initalPositionSameY, miniMapPositionSameY);

        if (distance > maxDistance)
        {
            transform.position = (initalPositionSameY - miniMapPositionSameY).normalized * maxDistance + miniMapPositionSameY;
            transform.localPosition = new Vector3(transform.localPosition.x, initialLocalPosition.y, transform.localPosition.z);
            //transform.position = new Vector3(transform.position.x, initalPosition.y, transform.position.z); //volver a aplicarle la altura inicial
        }
        else
        {
            transform.localPosition = initialLocalPosition;
            //transform.position = initalPosition;
        }
    }
}
