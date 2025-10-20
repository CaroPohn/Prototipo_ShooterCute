using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ImageSaturationManager: MonoBehaviour
{
    [SerializeField] private float saturation = 1;
    [SerializeField] private Image image;
    private float _ActualSaturation;
    private MaterialPropertyBlock block;
    void SetSaturation()
    {
        if (saturation != _ActualSaturation)
        {
            Material mat = image.material;
            _ActualSaturation = saturation;
            mat.SetFloat("_Saturation", _ActualSaturation);
            image.material = mat;

            /*renderer.GetPropertyBlock(block);
            block.SetFloat(propertyName, value);
            renderer.SetPropertyBlock(block);*/
        }
    }

    private void Awake()
    {
        _ActualSaturation = image.material.GetFloat("_Saturation");
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
