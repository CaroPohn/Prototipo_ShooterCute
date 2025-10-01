using UnityEngine;
using System.Collections;


public class HitEffectController : MonoBehaviour
{
    private GameObject enemy;

    [SerializeField] private GameObject particleDesintegrateVFX;

    //[Range(0f, 1f)]
    float hitAmount = 0f;

    [SerializeField] private Renderer[] renderers;
    public float effectDuration = 0.4f;
    private MaterialPropertyBlock block;
    private Coroutine resetRoutine;
    [ColorUsageAttribute(true, true)] [SerializeField] Color originalColor;
    bool colorWasChanged = false;

    string hitParameter = "_Hit_Amount";
    string dissolveParameter = "_Dissolve";
    [SerializeField] float dissolveDuration = 1f;


    void Start()
    {
        //rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();

        enemy = transform.parent.gameObject;
        
        //UpdateHitAmount();
    }

    void UpdatePropertyAmmount(string property, float amount)
    {
        foreach (Renderer rend in renderers) 
        {
            rend.GetPropertyBlock(block);
            block.SetFloat(property, amount);
            rend.SetPropertyBlock(block);
        }
    }


    void Update()
    {
        
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
        UpdatePropertyAmmount(hitParameter, hitAmount);
        float timer = 0f;

        while (timer < effectDuration)
        {
            timer += Time.deltaTime;
            hitAmount = Mathf.Lerp(1f, 0f, timer / effectDuration);
            UpdatePropertyAmmount(hitParameter, hitAmount);
            yield return null;
        }

        hitAmount = 0f;
        UpdatePropertyAmmount(hitParameter, hitAmount);
        if (colorWasChanged) ChangeRenderersColors(originalColor);
    }

    public void Dissolve()
    {
        StartCoroutine(DissolveRoutine());
    }

    public IEnumerator DissolveRoutine()
    {
        particleDesintegrateVFX.gameObject.SetActive(true);

        float dissolveAmount = 0f;
        UpdatePropertyAmmount(dissolveParameter, dissolveAmount);
        float timer = 0f;

        while (timer < dissolveDuration)
        {
            timer += Time.deltaTime;
            dissolveAmount = Mathf.Lerp(1f, 0f, 1-(timer / dissolveDuration));
            UpdatePropertyAmmount(dissolveParameter, dissolveAmount);
            yield return null;
        }

        Debug.Log("Desintegrado");

        dissolveAmount = 1f;
        UpdatePropertyAmmount(dissolveParameter, dissolveAmount);

        Destroy(enemy);
    }
}
