using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class EggGlowManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float glowIntensity = 1;
    private MaterialPropertyBlock block;
    [SerializeField] Renderer rend;
    [SerializeField] VisualEffect vfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!IsAwake()) SetUp();
    }
    void SetUp()
    {
        block = new MaterialPropertyBlock();
    }
    bool IsAwake()
    {
        return block != null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public float GlowIntensity
    {
        get { return glowIntensity; }
        set
        {
            if (glowIntensity != value)
            {
                glowIntensity = Mathf.Clamp(value, 0, 1) ;
                UpdateGlow(glowIntensity);
            }
        }
    }
    private void OnValidate()
    {
        if (!IsAwake()) SetUp();
        UpdateGlow(glowIntensity);
    }
    void UpdateGlow(float value)
    {
        rend.GetPropertyBlock(block);
        block.SetFloat("_Glow_Intensity", value);
        rend.SetPropertyBlock(block);
        vfx.SetFloat("Glow Global Scale", value);
    }
}
