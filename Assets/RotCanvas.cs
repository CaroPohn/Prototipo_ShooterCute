using UnityEngine;

public class RotCanvas : MonoBehaviour
{
    private GameObject cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(cam.transform.position - transform.position);
    }
}
