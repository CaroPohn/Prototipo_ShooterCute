using UnityEngine;

public class SpaceshipZoneSelectorUI : MonoBehaviour
{
    [SerializeField] SpaceshipZone[] spaceshipZonesInOrder;
    SpaceshipZone currentZone = SpaceshipZone.WorldSelect;
    int index = 0;
    [SerializeField] GameObject WorldselectUI;
    [SerializeField] GameObject loadoutUI;
    [SerializeField] CameraChanger camChanger;
    [SerializeField] ZoneChangerUI zoneChangerUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void UpdateCanvas()
    {
        zoneChangerUI.UpdateZoneChanger(currentZone);
        if (currentZone == SpaceshipZone.WorldSelect)
        {
            WorldselectUI.SetActive(true);
            loadoutUI.SetActive(false);
        }
        else if(currentZone == SpaceshipZone.Loadout)
        {
            WorldselectUI.SetActive(false);
            loadoutUI.SetActive(true);
        }
        else
        {
            WorldselectUI.SetActive(false);
            loadoutUI.SetActive(false);
        }

    }
    public void MoveLeft()
    {
        index--;
        if (index < 0)
        {
            index = spaceshipZonesInOrder.Length - 1;
        }
        currentZone = spaceshipZonesInOrder[index];
        UpdateCanvas();
        camChanger.ChangeCameraTo(currentZone);
    }
    public void MoveRight()
    {
        index++;
        if (index >= spaceshipZonesInOrder.Length)
        {
            index = 0;
        }
        currentZone = spaceshipZonesInOrder[index];
        UpdateCanvas();
        camChanger.ChangeCameraTo(currentZone);
    }
    void UpdateIndex(SpaceshipZone newSpaceshipZone)
    {
        for (int i = 0; i < spaceshipZonesInOrder.Length; i++)
        {
            if(newSpaceshipZone == spaceshipZonesInOrder[i])
            {
                index = i;
                i += spaceshipZonesInOrder.Length;
            }
        }
    }
    public void GoToLoadout()
    {
        currentZone = SpaceshipZone.Loadout;
        UpdateIndex(currentZone);
        UpdateCanvas();
        camChanger.ChangeCameraTo(currentZone);
    }
    public void GoToWorldSelect()
    {
        currentZone = SpaceshipZone.WorldSelect;
        UpdateIndex(currentZone);
        UpdateCanvas();
        camChanger.ChangeCameraTo(currentZone);
    }
    public void GoToReady()
    {
        currentZone = SpaceshipZone.Ready;
        UpdateIndex(currentZone);
        UpdateCanvas();
        camChanger.ChangeCameraTo(currentZone);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
