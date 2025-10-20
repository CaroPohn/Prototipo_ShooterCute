using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EggStation : MonoBehaviour
{
    [SerializeField] Renderer stationRenderer;
    [SerializeField] float timeToDesintegrate;
    [SerializeField] EggShield eggShield;
    [SerializeField] GameObject spawnEffectGO;
    private MaterialPropertyBlock block;

    
    private void Start()
    {
        block = new MaterialPropertyBlock();
    }

    void UpdateRendererPropertyValue(Renderer renderer, float value, string propertyName)
    {
        renderer.GetPropertyBlock(block);
        block.SetFloat(propertyName, value);
        renderer.SetPropertyBlock(block);
    }

    public void UpdateEggFadeProgress(float progress)
    {
        UpdateRendererPropertyValue(stationRenderer, progress, "_Desintegration");
    }

    

    public void Desintegrate()
    {
        StartCoroutine("DesintegrateCoroutine");
        
        if(eggShield != null )
        {
            eggShield.Desintegrate();
        }
    }
    public void SpawnShield()
    {
        if (eggShield != null)
        {
            eggShield.Appear();
        }
    }
    public void SpawnDirt()
    {
        if(spawnEffectGO != null)
        {
            spawnEffectGO.SetActive(true);
        }
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
    
}
