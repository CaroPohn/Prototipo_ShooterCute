using UnityEngine;
using System.Collections;


public class HitEffectController : MonoBehaviour
{
    //[Range(0f, 1f)]
    float hitAmount = 0f;

    [SerializeField] private Renderer[] renderers;
    public float effectDuration = 0.4f;
    private MaterialPropertyBlock block;
    private bool isPlayerInside = false;
    private Coroutine resetRoutine;
    [ColorUsageAttribute(true, true)] [SerializeField] Color originalColor;
    bool colorWasChanged = false;

    void Start()
    {
        //rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        
        //UpdateHitAmount();
    }

    void UpdateHitAmount()
    {
        foreach (Renderer rend in renderers) 
        {
            rend.GetPropertyBlock(block);
            block.SetFloat("_HitAmount", hitAmount);
            rend.SetPropertyBlock(block);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetHit();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetHit(Color.green);
        }
    }
    public void GetHit()
    {
        if (resetRoutine != null) 
        {
            StopCoroutine(resetRoutine);
        }
        resetRoutine = StartCoroutine(HitEffect());
    }
    public void GetHit(Color color)
    {
        ChangeRenderersColors(color);
        colorWasChanged = true;
        GetHit();
    }
    void ChangeRenderersColors(Color color)
    {
        foreach (Renderer rend in renderers)
        {
            rend.GetPropertyBlock(block);
            block.SetColor("_Color", color);
            rend.SetPropertyBlock(block);
        }
    }
    IEnumerator HitEffect()
    {
        hitAmount = 1f;
        UpdateHitAmount();
        float timer = 0f;

        while (timer < effectDuration)
        {
            timer += Time.deltaTime;
            hitAmount = Mathf.Lerp(1f, 0f, timer / effectDuration);
            UpdateHitAmount();
            yield return null;
        }

        hitAmount = 0f;
        UpdateHitAmount();
        if (colorWasChanged) ChangeRenderersColors(originalColor);
    }

}
