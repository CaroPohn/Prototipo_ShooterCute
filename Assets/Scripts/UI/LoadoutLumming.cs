using UnityEngine;
using UnityEngine.UI;

public class LoadoutLumming : MonoBehaviour
{
    [SerializeField] private Lumming lumming;
    [SerializeField] private Image cellImage;
    private LoadoutUI loadoutUI;
    bool blocked = false;


    public Lumming lummingInSlot
    {
        get
        {
            return lumming;
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
    public void BlockOption()
    {
        blocked = true;
        cellImage.color = Color.red;
    }
    public void UnlockOption()
    {
        blocked = false;
        cellImage.color = Color.white;
    }
    public void MarkAsSelected()
    {
        cellImage.color = Color.yellow;
    }
}
