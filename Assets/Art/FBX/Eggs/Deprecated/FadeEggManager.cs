using System.Collections.Generic;
using UnityEngine;

public class FadeEggManager : MonoBehaviour
{
    [SerializeField] List<Renderer> corruptedEggRenderer;
    private MaterialPropertyBlock block;

    private void Start()
    {
        block = new MaterialPropertyBlock();
    }
    
    public void UpdateEggFadeProgress(float progress)
    {
        foreach (var i in corruptedEggRenderer)
        {
            Debug.Log("LLAMADA   " + i);

            i.GetPropertyBlock(block);
            block.SetFloat("_Desintegration", progress);
            i.SetPropertyBlock(block);
        }
    }
}
