using UnityEngine;

public class LoadoutUI : MonoBehaviour
{
    [SerializeField] GameObject posibilities;
    [SerializeField] LummingSlots weaponSlot;
    [SerializeField] LummingSlots abilitySlot;
    [SerializeField] LoadoutWarningsUI warnings;
    LummingSlots currentSelectedSlot = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
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
        //Send updated data to summary
    }
    void DisplayEmptyWarning()
    {
        warnings.EmptySlotWarning();
    }
    void DisplaySameLummingWarning()
    {
        warnings.SameLummingWarning();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
