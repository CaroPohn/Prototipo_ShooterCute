using UnityEngine;

public class VFXScaleByDistance : MonoBehaviour
{
    public Transform player;
    public float scaleFactor = 0.1f;
    public float minScale = 0.5f;
    public float maxScale = 3f;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        float scale = 1 + distance * scaleFactor;
        scale = Mathf.Clamp(scale, minScale, maxScale);

        transform.localScale = initialScale * scale;
    }
}
