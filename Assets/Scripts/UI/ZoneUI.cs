using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.ShaderGraph.Internal;
using Unity.VisualScripting;

public class ZoneUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zoneTMP;
    [SerializeField] Color selectedColor;
    [SerializeField] Color nonselectedColor;
    [SerializeField] float timeForTextTransition;
    [SerializeField] float nonselectedFontSize = 28;
    [SerializeField] float selectedFontSize = 32;
    [SerializeField] float maxGlowPower = 0.18f;
    [SerializeField] float blinkTime = 0.07f;
    [SerializeField] Image selectedRingImage;
    [SerializeField] float delayBeforeBlinks = 0.5f;
    [SerializeField] float startingHeight = -22;
    [SerializeField] float endHeight = -13;
    [SerializeField] Image dotImage;
    [SerializeField] Sprite unlockedDot;
    [SerializeField] Sprite blockedDot;

    [SerializeField] float timeToPrgoressToNextStage = 0.1f;

    Coroutine currentRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectThisZone();
        }
    }
    public void SelectThisZone()
    {
        if(currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(SelectDotCoroutine());
    }
    public void BlockThisZone()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        dotImage.sprite = blockedDot;
        currentRoutine = StartCoroutine(UnselectDotCoroutine());
    }
    public void LeaveThisZone()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(UnselectDotCoroutine());
    }
    IEnumerator SelectDotCoroutine()
    {
        dotImage.sprite = unlockedDot;
        zoneTMP.fontMaterial.EnableKeyword("GLOW_ON");
        float timer = 0;
        Color whiteWithTransparent = Color.white;
        whiteWithTransparent.a = 0;
        while (timer<timeForTextTransition) 
        {
            timer += Time.deltaTime;
            float normalizedProgress = timer / timeForTextTransition;
            zoneTMP.color = Color.Lerp(nonselectedColor, selectedColor, normalizedProgress);
            zoneTMP.fontSize = Mathf.Lerp(nonselectedFontSize,selectedFontSize, normalizedProgress);
            zoneTMP.fontMaterial.SetFloat("_GlowPower", normalizedProgress * maxGlowPower);
            zoneTMP.rectTransform.localPosition = new Vector3(0,Mathf.Lerp(startingHeight,endHeight, normalizedProgress),0);

            selectedRingImage.color = Color.Lerp(Color.white, whiteWithTransparent, normalizedProgress);

            yield return null;
        }


        zoneTMP.color = selectedColor;
        Color tempColor = zoneTMP.color;
        tempColor.a = 0;
        zoneTMP.color = tempColor;
        selectedRingImage.color = whiteWithTransparent;
        yield return new WaitForSeconds(blinkTime);
        tempColor.a = 1;
        zoneTMP.color = tempColor;
        selectedRingImage.color = Color.white;
        yield return new WaitForSeconds(blinkTime);
        tempColor.a = 0;
        zoneTMP.color = tempColor;
        selectedRingImage.color = whiteWithTransparent;
        yield return new WaitForSeconds(blinkTime);
        tempColor.a = 1;
        selectedRingImage.color = Color.white;
        zoneTMP.color = tempColor;


    }
    
    IEnumerator UnselectDotCoroutine()
    {
        
        float timer = 0;
        Color whiteWithTransparent = Color.white;
        whiteWithTransparent.a = 0;

        while (timer < timeToPrgoressToNextStage)
        {
            timer += Time.deltaTime;
            float normalizedProgress = timer / timeToPrgoressToNextStage;
            selectedRingImage.color = Color.Lerp(Color.white, whiteWithTransparent, normalizedProgress);

            zoneTMP.color = Color.Lerp(selectedColor ,nonselectedColor, normalizedProgress);
            zoneTMP.fontSize = Mathf.Lerp(selectedFontSize,nonselectedFontSize, normalizedProgress);
            zoneTMP.fontMaterial.SetFloat("_GlowPower", Mathf.Lerp(maxGlowPower, 0, normalizedProgress));
            zoneTMP.rectTransform.localPosition = new Vector3(0, Mathf.Lerp(endHeight,startingHeight, normalizedProgress), 0);
            yield return null;

        }
        zoneTMP.fontMaterial.DisableKeyword("GLOW_ON");

    }
}
