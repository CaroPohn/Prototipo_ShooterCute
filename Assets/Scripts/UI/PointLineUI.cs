using UnityEngine;
using UnityEngine.UI;

public class PointLineUI : MonoBehaviour
{
    [SerializeField] Image pointLineImage;
    private void OnEnable()
    {
        pointLineImage.material = new Material(pointLineImage.material);
    }

    public void SetProgress(float progress)
    {
        pointLineImage.material.SetFloat("_Progress", progress);
    }
}
