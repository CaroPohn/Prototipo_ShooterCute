using UnityEngine;
using UnityEngine.UI;

public class LoadoutSummery : MonoBehaviour
{
    [SerializeField] LummingCell weaponCell;
    [SerializeField] LummingCell abilityCell;
    [SerializeField] GameObject container;

    private void Start()
    {
        UpdateWeaponImage(Lumming.None);
        UpdateAbilityImage(Lumming.None);
    }
    public void HideScreen()
    {
        container.SetActive(false);
    }
    public void ShowScreen()
    {
        container.SetActive(true);
    }

    public void UpdateWeaponImage(Lumming newLumming)
    {
        if (newLumming == Lumming.None)
        {
            weaponCell.ShowNonSelected();
            weaponCell.HideLumming();
        }

        else
        {
            weaponCell.ChangeLumming(newLumming);
            weaponCell.ShowAsWeapon();
        }
    }
    public void UpdateAbilityImage(Lumming newLumming)
    {
        if (newLumming == Lumming.None) 
        {
            abilityCell.ShowNonSelected();
            abilityCell.HideLumming();
        } 
        else 
        {
            abilityCell.ChangeLumming(newLumming);
            abilityCell.ShowAsAbility();
        }

    }
}
