using System.Drawing;
using UnityEngine;

public class FadeEggManager : MonoBehaviour
{
    [SerializeField] Renderer corruptedEggRenderer;
    private MaterialPropertyBlock block;
    private void Start()
    {
        block = new MaterialPropertyBlock();
    }
    
    public void UpdateEggFadeProgress(float progress)
    {
        corruptedEggRenderer.GetPropertyBlock(block);
        block.SetFloat("_Fade_Progress", progress);
        corruptedEggRenderer.SetPropertyBlock(block);
    }
}
