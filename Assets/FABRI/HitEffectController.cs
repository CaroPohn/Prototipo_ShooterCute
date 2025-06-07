using UnityEngine;
using System.Collections;


public class HitEffectController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float hitAmount = 0f;

    private Renderer rend;
    private MaterialPropertyBlock block;
    private bool isPlayerInside = false;
    private Coroutine resetRoutine;

    void Start()
    {
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        UpdateHitAmount();
    }

    public void UpdateHitAmount()
    {
        rend.GetPropertyBlock(block);
        block.SetFloat("_HitAmount", hitAmount);
        rend.SetPropertyBlock(block);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))  // ← CAMBIO AQUÍ
        {
            if (resetRoutine != null)
                StopCoroutine(resetRoutine);

            resetRoutine = StartCoroutine(HitEffect());
        }
    }

    IEnumerator HitEffect()
    {
        hitAmount = 1f;
        UpdateHitAmount();

        float duration = 0.4f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            hitAmount = Mathf.Lerp(1f, 0f, timer / duration);
            UpdateHitAmount();
            yield return null;
        }

        hitAmount = 0f;
        UpdateHitAmount();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
