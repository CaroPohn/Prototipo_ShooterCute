using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutUI : MonoBehaviour
{
    [SerializeField] SummaryUI summaryUI;
    [SerializeField] LummingOnTable lummingOnTable;

    [SerializeField] LoadoutTabButton weaponTabUI;
    [SerializeField] LoadoutTabButton abilityTabUI;
    [SerializeField] LoadoutLumming[] selectableLummingsSlots;

    [SerializeField] LoadoutSummery loadoutSummaryUI;

    [SerializeField] Button confirmButton;

    TabData currentTab;
    TabData weaponTab;
    TabData abilityTab;
    TabData[] tabs;

    public Lumming savedWeaponLumming 
    {
        get
        {
            return weaponTab.savedLumming;
        }
    }
    public Lumming savedAbilityLumming
    {
        get
        {
            return abilityTab.savedLumming;
        }
    }

    private void Start()
    {
        weaponTab = new TabData(weaponTabUI,Lumming.None);
        abilityTab = new TabData(abilityTabUI,Lumming.None);

        currentTab = weaponTab;
        tabs = new TabData[2] {weaponTab,abilityTab};
    }

    public void TabPressed(LoadoutTabButton tabPressed)
    {
        foreach(TabData tab in tabs)
        {
            if(tabPressed != tab.tabUI)
            {
                tab.tabUI.SendToTheBack();
                tab.tabUI.gameObject.transform.SetAsFirstSibling();
            }
            else
            {
                tab.tabUI.BringToFront();
                currentTab = tab;
                break;
            }
        }
        UpdateSelectableLummingSlots();

    }
    void UpdateSelectableLummingSlots()
    {
      
        foreach(LoadoutLumming selectableLummingSlot in selectableLummingsSlots)
        {
            if (currentTab.savedLumming == selectableLummingSlot.lummingInSlot) selectableLummingSlot.MarkAsSelected();
            else if (LummingIsUsedOnOtherTabs(selectableLummingSlot.lummingInSlot)) selectableLummingSlot.BlockOption();
            else selectableLummingSlot.UnlockOption();
        }
    }
    bool LummingIsUsedOnOtherTabs(Lumming lummingToSearch)
    {
        bool lummingFound = false;
        foreach(TabData tab in tabs)
        {
            if(tab != currentTab)
            {
                lummingFound = (tab.savedLumming == lummingToSearch);
                if (lummingFound) break;
            }
        }
        return lummingFound;
        
    }
    public void SlotPressed(LoadoutLumming lummingSlot)
    {
        Debug.Log("Pressed on lumming slot: " + lummingSlot.lummingInSlot.ToString() + " ID: " + (int)lummingSlot.lummingInSlot);
        //If already selected, deselect it
        if (currentTab.savedLumming == lummingSlot.lummingInSlot)
        {
            currentTab.savedLumming = Lumming.None;
            lummingSlot.UnlockOption();
        }
        else 
        {
            //Deselect previous chosen lumming
            if (currentTab.savedLumming != Lumming.None)
            {
                LoadoutLumming foundLummingSlot = Array.Find(selectableLummingsSlots, p => (p.lummingInSlot == currentTab.savedLumming));
                if(foundLummingSlot != null) foundLummingSlot.UnlockOption();
            }
            lummingSlot.MarkAsSelected();
            currentTab.savedLumming = lummingSlot.lummingInSlot;
        }
        UpdateLoadoutSummery();
        UpdateConfirmButton();
    }

    void UpdateLoadoutSummery()
    {
        loadoutSummaryUI.UpdateAbilityImage(abilityTab.savedLumming);
        loadoutSummaryUI.UpdateWeaponImage(weaponTab.savedLumming);
    }
    void UpdateConfirmButton()
    {
        if (weaponTab.savedLumming != Lumming.None && abilityTab.savedLumming != Lumming.None) confirmButton.interactable = true;
        else confirmButton.interactable = false;
    }

    public void SaveChanges()
    {
        summaryUI.UpdateLoadout(weaponTab.savedLumming, abilityTab.savedLumming);
        //saveChangesButton.PlayAnimationDisabled();
        FindFirstObjectByType<SpaceshipZoneSelectorUI>().MoveRight();
    }
}
public class TabData
{
    public LoadoutTabButton tabUI;
    public Lumming savedLumming;

    public TabData(LoadoutTabButton tabUIButton, Lumming lummingToSave)
    {
        tabUI = tabUIButton;
        savedLumming = lummingToSave;
    }
}