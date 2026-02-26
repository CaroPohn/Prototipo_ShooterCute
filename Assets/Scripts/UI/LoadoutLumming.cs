using UnityEngine;
using UnityEngine.UI;

public class LoadoutLumming : MonoBehaviour
{
    [SerializeField] private Lumming lumming;
    [SerializeField] private LummingCell cell;
    private LoadoutUI loadoutUI;
    [SerializeField] bool blocked = false;


    public Lumming lummingInSlot
    {
        get
        {
            return lumming;
        }
    }
    private void OnEnable()
    {
        if (blocked) 
        {
            cell.ShowBlocked();
        }
        else
        {
            cell.ChangeLumming(lumming);
            cell.ShowNonSelected();
        }
    }
    public void Clicked()
    {
        if (blocked) return;
        if(loadoutUI == null) 
        {
            loadoutUI = GameObject.FindFirstObjectByType<LoadoutUI>();
        }
        loadoutUI.SlotPressed(this);
    }
    public void SelectAsWeapon()
    {
        cell.ShowAsWeapon();
    }
    public void SelectAsAbility()
    {
        cell.ShowAsAbility();
    }
    public void Deselect()
    {
        cell.ShowNonSelected();
    }
}
