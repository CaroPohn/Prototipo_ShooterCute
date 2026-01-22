using UnityEngine;
using UnityEngine.UI;

public class LoadoutSummery : MonoBehaviour
{
    [SerializeField] Sprite[] lummingSprites;
    [SerializeField] Image weaponLummingImage;
    [SerializeField] Image abilityLummingImage;

    public void UpdateWeaponImage(Lumming newLumming)
    {
        Debug.Log("Updated to lumming ID: " + (int)newLumming);
        weaponLummingImage.sprite = lummingSprites[(int)newLumming];
    }
    public void UpdateAbilityImage(Lumming newLumming)
    {
        abilityLummingImage.sprite = lummingSprites[(int)newLumming];
    }
}
