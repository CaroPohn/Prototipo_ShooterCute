using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgressBar : MonoBehaviour
{
    [SerializeField] Image fillBarImage;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float animationDuration;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float timeForInitialFadeIn;
    [SerializeField] float fadeOutDuration;
    [SerializeField] float blinkDuration;
    [SerializeField] int numberOfBlinks;
   
    float currentProgress = 0;
    public void DisplayProgressBar()
    {
        StopAllCoroutines();
        fillBarImage.material.SetFloat("_GlowIntensity", 0);
        fillBarImage.material.SetFloat("_Progress", 0);
        StartCoroutine(DisplayAnimationCoroutine());
    }
    public void HideProgressBar()
    {
        StopAllCoroutines();
        fillBarImage.material.SetFloat("_GlowIntensity", 0);
        StartCoroutine(HideAnimationCoroutine());
    }
    
    public void UpdateProgress(float progress) 
    {
        if (currentProgress == progress) return;
        fillBarImage.material.SetFloat("_Progress", Mathf.Clamp01(progress));
        StopAllCoroutines();
        fillBarImage.material.SetFloat("_GlowIntensity", 0);
        StartCoroutine(GlowEffCoroutine());
    }
    IEnumerator GlowEffCoroutine()
    {
        float timer = 0;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            fillBarImage.material.SetFloat("_GlowIntensity", curve.Evaluate(Mathf.Clamp01(timer / animationDuration)));
            yield return null;
        }
    }
    IEnumerator DisplayAnimationCoroutine()
    {
        float timer = 0;
        while (timer < timeForInitialFadeIn)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0,1, Mathf.Clamp01(timer / timeForInitialFadeIn)); 
            yield return null;
        }
        int blinks = 0;
        while(blinks < numberOfBlinks)
        {
            canvasGroup.alpha = 0;
            yield return new WaitForSeconds(blinkDuration);
            canvasGroup.alpha = 1;
            yield return new WaitForSeconds(blinkDuration);
            blinks++;
        }
        
    }
    IEnumerator HideAnimationCoroutine()
    {
        float timer = 0;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, Mathf.Clamp01(timer / fadeOutDuration));
            yield return null;
        }
    }

}
