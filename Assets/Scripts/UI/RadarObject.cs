using UnityEngine;

public class RadarObject : MonoBehaviour
{
    private Transform miniMapTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!miniMapTransform) miniMapTransform = GameObject.Find("MiniMapCamera").transform;
        transform.rotation = miniMapTransform.rotation;
    }
}
