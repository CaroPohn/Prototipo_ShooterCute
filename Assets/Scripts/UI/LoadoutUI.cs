using UnityEngine;

public class LoadoutUI : MonoBehaviour
{
    [SerializeField] GameObject posibilities;
    [SerializeField] LummingSlots weaponSlot;
    [SerializeField] LummingSlots abilitySlot;
    [SerializeField] LoadoutWarningsUI warnings;
    [SerializeField] SummaryUI summaryUI;
    LummingSlots currentSelectedSlot = null;
    
    public void WeaponButtonPressed()
    {
        if(currentSelectedSlot == weaponSlot)
        {
            posibilities.SetActive(false);
            weaponSlot.StopActiveAnimation();
            currentSelectedSlot = null;
        }
        else
        {
            weaponSlot.PlayActiveAnimation();
            abilitySlot.StopActiveAnimation();
            currentSelectedSlot = weaponSlot;
            posibilities.SetActive(true);
        }
    }
    public void AbilityButtonPressed()
    {
        if (currentSelectedSlot == abilitySlot)
        {
            posibilities.SetActive(false);
            abilitySlot.StopActiveAnimation();
            currentSelectedSlot = null;
        }
        else
        {
            weaponSlot.StopActiveAnimation();
            abilitySlot.PlayActiveAnimation();
            currentSelectedSlot = abilitySlot;
            posibilities.SetActive(true);
        }
        
    }
    public void SlotPressed(int lumming)
    {
        currentSelectedSlot.ReplaceLumming((Lumming)lumming);
    }
    public void SaveChanges()
    {
        if(weaponSlot.GetCurrentLumming() == Lumming.None || abilitySlot.GetCurrentLumming() == Lumming.None)
        {
            DisplayEmptyWarning();
            return;
        }
        
        if(weaponSlot.GetCurrentLumming() == abilitySlot.GetCurrentLumming())
        {
            DisplaySameLummingWarning();
            return;
        }
        summaryUI.UpdateLoadout(weaponSlot.GetCurrentLumming(), abilitySlot.GetCurrentLumming());
    }
    void DisplayEmptyWarning()
    {
        warnings.EmptySlotWarning();
    }
    void DisplaySameLummingWarning()
    {
        warnings.SameLummingWarning();
    }
}
