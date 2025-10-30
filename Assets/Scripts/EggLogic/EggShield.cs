using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class EggShield : MonoBehaviour
{

    [SerializeField] List<Renderer> corruptedEggRenderers;
    [SerializeField] float timeToDesintegrate;
    [SerializeField] float timeToAppear;
    [SerializeField] float maxGlowWhenHit;
    [SerializeField] float timeForGlowToFadeOut;
    [SerializeField] VisualEffect vfx;
    Coroutine eggGetHitCoroutine;
    private MaterialPropertyBlock block;

    private void OnEnable()
    {
        EnemySoul.OnParticleDeath += GetHit;
    }
    private void OnDisable()
    {
        EnemySoul.OnParticleDeath -= GetHit;
    }
    private void Start()
    {
        block = new MaterialPropertyBlock();
    }
    
    void UpdateRenderersPropertyValue(List<Renderer> renderersList,float value, string propertyName)
    {
        foreach (Renderer renderer in renderersList)
        {
            renderer.GetPropertyBlock(block);
            block.SetFloat(propertyName, value);
            renderer.SetPropertyBlock(block);
        }
    }

    public void UpdateEggFadeProgress(float progress)
    {
        UpdateRenderersPropertyValue(corruptedEggRenderers, progress,"_Desintegration");
    }

    public void UpdateShieldHitGlow(float damage)
    {
        UpdateRenderersPropertyValue(corruptedEggRenderers, damage, "_Blend_Color_Opacity");
    }

    public void Desintegrate()
    {
        StartCoroutine("DesintegrateCoroutine");
        vfx.Stop();
    }

    public void Appear()
    {
        StartCoroutine("AppearCoroutine");
        vfx.Play();
    }

    public void GetHit()
    {
        if (eggGetHitCoroutine != null) StopCoroutine(eggGetHitCoroutine);
        eggGetHitCoroutine = StartCoroutine("ShieldGetHitCoroutine");

        AkUnitySoundEngine.PostEvent("Egg_Soul_Impact", gameObject);
    }
    IEnumerator ShieldGetHitCoroutine()
    {
        UpdateShieldHitGlow(maxGlowWhenHit);
        for (float f = 0; f < timeForGlowToFadeOut; f += Time.deltaTime)
        {
            UpdateShieldHitGlow(maxGlowWhenHit - (f / timeForGlowToFadeOut)* maxGlowWhenHit);
            yield return null;
        }
        UpdateShieldHitGlow(0);
    }
        IEnumerator DesintegrateCoroutine()
    {
        for (float f = 0; f < timeToDesintegrate; f += Time.deltaTime)
        {
            UpdateEggFadeProgress(f / timeToDesintegrate);
            yield return null;
        }
        UpdateEggFadeProgress(1f);
    }
    IEnumerator AppearCoroutine()
    {
        for (float f = 0; f < timeToAppear; f += Time.deltaTime)
        {
            UpdateEggFadeProgress(1 - (f / timeToAppear));
            yield return null;
        }
        UpdateEggFadeProgress(0f);
    }
}
