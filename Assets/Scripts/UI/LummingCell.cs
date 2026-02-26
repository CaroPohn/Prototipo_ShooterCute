using UnityEngine;
using UnityEngine.UI;

public class LummingCell : MonoBehaviour
{
    [SerializeField] private Lumming lumming;
    [SerializeField] private Image lummingImage;
    [SerializeField] private Sprite[] lummingCommonSprites;
    [SerializeField] private Sprite[] lummingWeaponSprites;
    [SerializeField] private Sprite[] lummingAbilitySprites;
    [SerializeField] private Image cellImage;
    [SerializeField] private Sprite cellSpriteBlocked;
    [SerializeField] private Sprite cellSprite;
    [SerializeField] private Sprite cellSpriteWeapon;
    [SerializeField] private Sprite cellSpriteAbility;
    [SerializeField] private Image glowImage;
    [SerializeField] private Sprite glowWeapon;
    [SerializeField] private Sprite glowAbility;


    public void HideLumming()
    {
        lummingImage.gameObject.SetActive(false);
    }
    public void ShowLumming()
    {
        lummingImage.gameObject.SetActive(true);
    }
    public void ShowAsWeapon()
    {
        ShowLumming();
        glowImage.gameObject.SetActive(true);
        glowImage.sprite = glowWeapon;
        lummingImage.sprite = lummingWeaponSprites[(int)lumming];
        cellImage.sprite = cellSpriteWeapon;
    }
    public void ShowAsAbility()
    {
        ShowLumming();
        glowImage.gameObject.SetActive(true);
        glowImage.sprite = glowAbility;
        lummingImage.sprite = lummingAbilitySprites[(int)lumming]; 
        cellImage.sprite= cellSpriteAbility;
    }
    public void ShowNonSelected()
    {
        lummingImage.sprite = lummingCommonSprites[(int)lumming];
        cellImage.sprite = cellSprite;
        glowImage.gameObject.SetActive(false);
    }
    public void ShowBlocked()
    {
        HideLumming();
        cellImage.sprite = cellSpriteBlocked;
        glowImage.gameObject.SetActive(false);
    }
    public void ChangeLumming(Lumming lumming)
    {
        this.lumming = lumming;
    }

}
