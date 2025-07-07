using UnityEngine;

public class SpinLoadout : MonoBehaviour
{
    [SerializeField] GameObject gunWithLumming;
    [SerializeField] float rotationSpeed;
    [SerializeField] float levitatingSpeed;
    [SerializeField] Renderer cylinderRend;
    [SerializeField] float lightIntensity;
    [SerializeField] float lightFrequency;
    float initialAltitude;
    float initialIntensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialAltitude = gunWithLumming.transform.position.y;
        initialIntensity = cylinderRend.material.GetFloat("_Intensity");
    }

    // Update is called once per frame
    void Update()
    {
        gunWithLumming.transform.Rotate(new Vector3(0, Mathf.Cos(Time.deltaTime)* rotationSpeed, 0));
        gunWithLumming.transform.position = new Vector3(gunWithLumming.transform.position.x, initialAltitude + Mathf.Cos(Time.time) * levitatingSpeed, gunWithLumming.transform.position.z);

        cylinderRend.material.SetFloat("_Intensity", initialIntensity + Mathf.Cos(Time.time * lightFrequency) * lightIntensity);
    }
}
