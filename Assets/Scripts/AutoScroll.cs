using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float speed = 0.1f;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = scrollRect.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        scrollRect.verticalNormalizedPosition -= speed * Time.deltaTime;
    }
}
