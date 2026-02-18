using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class MultiAlphaPulse : MonoBehaviour
{
    public Material[] materials;
    public VisualEffect vfx;
    public VisualEffect Frozen;

    [Header("Fade 1")]
    public float fadeTime1 = 0.2f;

    [Header("Fade 2 (Curved)")]
    public float fadeTime2 = 2f;
    public AnimationCurve fade2Curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Delay")]
    public float delayAfterFirst = 0.5f;

    public KeyCode key = KeyCode.Space;

    Coroutine routine;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (routine != null)
                StopCoroutine(routine);

            if (vfx != null)
                vfx.Play();
                Frozen.Play();

            routine = StartCoroutine(AlphaSequence());
        }
    }

    IEnumerator AlphaSequence()
    {
        yield return FadeLinear(0, 1, fadeTime1);
        yield return new WaitForSeconds(delayAfterFirst);
        yield return FadeCurved(1, 0, fadeTime2, fade2Curve);
    }

    IEnumerator FadeLinear(float from, float to, float time)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            float value = Mathf.Lerp(from, to, t);

            Apply(value);
            yield return null;
        }
    }

    IEnumerator FadeCurved(float from, float to, float time, AnimationCurve curve)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            float curveT = curve.Evaluate(t);       // 🔥 magia
            float value = Mathf.Lerp(from, to, curveT);

            Apply(value);
            yield return null;
        }
    }

    void Apply(float value)
    {
        foreach (var mat in materials)
        {
            mat.SetFloat("_Alpha", value*1.3f);
            mat.SetFloat("_FrostRing", value);
        }
    }
}
