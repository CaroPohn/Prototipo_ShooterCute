using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LummingSlots : MonoBehaviour
{
    [SerializeField] Image lummingImage;
    [SerializeField] Image boxImage;
    [SerializeField] Sprite[] lummingImages;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite unselectedSprite;
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    [SerializeField] TextMeshProUGUI CategoryTMP;
    [SerializeField] TextMeshProUGUI NameTMP;
    Lumming currentLuming = Lumming.None;

    public void ReplaceLumming(Lumming lumming)
    {
        lummingImage.sprite = lummingImages[(int)lumming];
        currentLuming = lumming;
        NameTMP.text = lumming.ToString();
    }

    public Lumming GetCurrentLumming()
    {
        return currentLuming;
    }

    public void PlayActiveAnimation()
    {
        boxImage.sprite = selectedSprite;
        boxImage.SetNativeSize();
        CategoryTMP.color = selectedColor;
        NameTMP.color = selectedColor;
        lummingImage.color = selectedColor;
    }

    public void StopActiveAnimation()
    {
        boxImage.sprite = unselectedSprite;
        boxImage.SetNativeSize();
        CategoryTMP.color = unselectedColor;
        NameTMP.color = unselectedColor;
        lummingImage.color = unselectedColor;
    }
}
