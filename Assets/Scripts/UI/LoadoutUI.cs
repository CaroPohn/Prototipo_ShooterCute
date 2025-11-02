using Unity.VisualScripting;
using UnityEngine;

public class LoadoutUI : MonoBehaviour
{
    [SerializeField] LummingOptionsUI optionsPanel;
    [SerializeField] LummingSlots weaponSlot;
    [SerializeField] LummingSlots abilitySlot;
    [SerializeField] SummaryUI summaryUI;
    LummingSlots currentSelectedSlot = null;
    [SerializeField] Ready_Button_Spaceship saveChangesButton;
    [SerializeField] LummingOnTable lummingOnTable;
    Lumming savedWeaponLumming = Lumming.None;
    Lumming savedAbilityLumming = Lumming.None;
    public void WeaponButtonPressed()
    {
        if(currentSelectedSlot == weaponSlot)
        {
            optionsPanel.HideOptions();
            weaponSlot.StopActiveAnimation();
            currentSelectedSlot = null;
        }
        else
        {
            weaponSlot.PlayActiveAnimation();
            abilitySlot.StopActiveAnimation();
            currentSelectedSlot = weaponSlot;
            optionsPanel.ShowOptions();
        }
    }
    public void AbilityButtonPressed()
    {
        if (currentSelectedSlot == abilitySlot)
        {
            optionsPanel.HideOptions();
            abilitySlot.StopActiveAnimation();
            currentSelectedSlot = null;
        }
        else
        {
            weaponSlot.StopActiveAnimation();
            abilitySlot.PlayActiveAnimation();
            currentSelectedSlot = abilitySlot;
            optionsPanel.ShowOptions();
        }
        
    }
    public void SlotPressed(int lumming)
    {
        currentSelectedSlot.ReplaceLumming((Lumming)lumming);
        if(currentSelectedSlot == weaponSlot)
        {
            lummingOnTable.UpdateLummingOnTable((Lumming)lumming);
        }
        if (ButtonShouldActivate())
        {
            saveChangesButton.PlayAnimationReady();
        }
        else saveChangesButton.PlayAnimationDisabled();
        optionsPanel.HideOptions();
        weaponSlot.StopActiveAnimation();
        abilitySlot.StopActiveAnimation();
    }
    bool ButtonShouldActivate()
    {
        if (weaponSlot.GetCurrentLumming() == Lumming.None || abilitySlot.GetCurrentLumming() == Lumming.None) return false;
        if (weaponSlot.GetCurrentLumming() == abilitySlot.GetCurrentLumming()) return false;
        if (weaponSlot.GetCurrentLumming() == savedWeaponLumming || abilitySlot.GetCurrentLumming() == savedAbilityLumming) return false;
        return true;
    }
    public void SaveChanges()
    {
        savedWeaponLumming = weaponSlot.GetCurrentLumming();
        savedAbilityLumming = abilitySlot.GetCurrentLumming();
        summaryUI.UpdateLoadout(savedWeaponLumming, savedAbilityLumming);
        saveChangesButton.PlayAnimationDisabled();
    }
}
