using UnityEngine;
using UnityEngine.UI;

public class UIGradientAppearManager : MonoBehaviour
{
    [SerializeField] private float fill = 1;
    [SerializeField] private Image image;
    private float _ActualFill;
    private MaterialPropertyBlock block;
    void SetSaturation()
    {
        if (fill != _ActualFill)
        {
            Material mat = image.material;
            _ActualFill = fill;
            mat.SetFloat("_Fill", _ActualFill);
            image.material = mat;
        }
    }

    private void Awake()
    {
        _ActualFill = image.material.GetFloat("_Fill");
    }

    private void LateUpdate()
    {
        SetSaturation();
    }

    private void OnValidate()
    {
        SetSaturation();
    }
}
