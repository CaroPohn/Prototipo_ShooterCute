using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutUI : SpaceShipZoneScreen
{
    [SerializeField] SummaryUI summaryUI;
    [SerializeField] LummingOnTable lummingOnTable;
    [SerializeField] LoadoutSummery loadoutSummaryUI;

    [SerializeField] Button confirmButton;
    [SerializeField] GameObject confirmButtonGlowGO;
    [SerializeField] ChooseButton weaponChooseButton;
    [SerializeField] ChooseButton abilityChooseButton;
    [SerializeField] LummingDescription lummingDescriptions;
    [SerializeField] LoadoutLumming[] lummingSlots;
    [SerializeField] LoadoutLumming firstSlot;

    [SerializeField] GameObject lummingSelectionContainer;
   

    Lumming savedLumming_Weapon;
    Lumming savedLumming_Ability;

    Lumming currentLumming = Lumming.Bomb;
    LoadoutLumming lastSlotPressed;


    public Lumming savedWeaponLumming 
    {
        get
        {
            return savedLumming_Weapon;
        }
    }
    public Lumming savedAbilityLumming
    {
        get
        {
            return savedLumming_Ability;
        }
    }

    private void OnEnable()
    {
        if(lastSlotPressed == null)
        {
            lastSlotPressed = firstSlot;
        }
        SlotPressed(lastSlotPressed);
    }

    public override void ShowAllScreens()
    {
        lummingDescriptions.ShowScreen();
        loadoutSummaryUI.ShowScreen();
        lummingSelectionContainer.SetActive(true);
    }
    public override void HideAllScreens() 
    {
        lummingDescriptions.HideScreen();
        loadoutSummaryUI.HideScreen();
        lummingSelectionContainer.SetActive(false);
    }
    public void SlotPressed(LoadoutLumming lummingSlot)
    {
        currentLumming = lummingSlot.lummingInSlot;
        lummingOnTable.UpdateLummingOnTable(currentLumming);
        UpdateLummingDataShown();
        UpdateChooseButtons();
    }
    public void LummingChosenAsWeapon()
    {
        if(currentLumming == Lumming.None)
        {
            Debug.LogWarning("Cannot chose an empty lumming as weapon");
            return;
        }
        savedLumming_Weapon = currentLumming;
        if(savedLumming_Ability == currentLumming)
        {
            savedLumming_Ability = Lumming.None;
        }
        UpdateChooseButtons();
        UpdateConfirmButton();
        UpdateLoadoutSummery();
        UpdateCells();
    }
    public void LummingChosenAsAbility()
    {
        if (currentLumming == Lumming.None)
        {
            Debug.LogWarning("Cannot chose an empty lumming as ability");
            return;
        }
        savedLumming_Ability = currentLumming;
        if (savedLumming_Weapon == currentLumming)
        {
            savedLumming_Weapon = Lumming.None;
        }
        UpdateChooseButtons();
        UpdateConfirmButton();
        UpdateLoadoutSummery();
        UpdateCells();
    }
    void UpdateCells()
    {
        foreach(LoadoutLumming lummingSlot in lummingSlots)
        {
            if(lummingSlot.lummingInSlot == savedLumming_Weapon)
            {
                lummingSlot.SelectAsWeapon();
            }
            else if(lummingSlot.lummingInSlot == savedLumming_Ability)
            {
                lummingSlot.SelectAsAbility();
            }
            else
            {
                lummingSlot.Deselect();
            }
        }
    }
    void UpdateChooseButtons()
    {
        if(savedLumming_Weapon == currentLumming)
        {
            weaponChooseButton.Disable();
        }
        else
        {
            weaponChooseButton.Enable();
        }
        if(savedLumming_Ability == currentLumming)
        {
            abilityChooseButton.Disable();
        }
        else
        {
            abilityChooseButton.Enable();
        }
    }
    void UpdateLummingDataShown()
    {
        lummingDescriptions.UpdateLummingDescription(currentLumming);
    }
    void UpdateLoadoutSummery()
    {
        loadoutSummaryUI.UpdateAbilityImage(savedLumming_Ability);
        loadoutSummaryUI.UpdateWeaponImage(savedLumming_Weapon);
    }
    void UpdateConfirmButton()
    {
        if (savedLumming_Ability != Lumming.None && savedLumming_Weapon != Lumming.None) 
        {
            confirmButton.interactable = true;
            confirmButtonGlowGO.SetActive(true);
        }
        else
        {
            confirmButton.interactable = false;
            confirmButtonGlowGO.SetActive(false);
        }
    }

    public void SaveChanges()
    {
        summaryUI.UpdateLoadout(savedLumming_Weapon, savedLumming_Ability);
        //saveChangesButton.PlayAnimationDisabled();
        FindFirstObjectByType<SpaceshipZoneSelectorUI>().MoveRight();
    }
}

