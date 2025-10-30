using UnityEngine;
using UnityEngine.UI;

public class LummingSlots : MonoBehaviour
{
    [SerializeField] Image lummingImage;
    [SerializeField] Image boxImage;
    [SerializeField] Sprite[] lummingImages;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite unselectedSprite;
    Lumming currentLuming = Lumming.None;
    public void ReplaceLumming(Lumming lumming)
    {
        lummingImage.sprite = lummingImages[(int)lumming];
        currentLuming = lumming;
    }
    public Lumming GetCurrentLumming()
    {
        return currentLuming;
    }
    public void PlayActiveAnimation()
    {
        boxImage.sprite = selectedSprite;
        boxImage.SetNativeSize();
    }
    public void StopActiveAnimation()
    {
        boxImage.sprite = unselectedSprite;
        boxImage.SetNativeSize();
    }
}
