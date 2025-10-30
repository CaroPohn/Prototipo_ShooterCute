using UnityEngine;

public class SpaceshipZoneSelectorUI : MonoBehaviour
{
    [SerializeField] SpaceshipZone[] spaceshipZonesInOrder;
    SpaceshipZone currentZone = SpaceshipZone.WorldSelect;
    int index = 0;
    [SerializeField] GameObject worldSelectLineGO;
    [SerializeField] GameObject loadoutLineGO;
    [SerializeField] GameObject readyLineGO;
    [SerializeField] GameObject WorldselectUI;
    [SerializeField] GameObject loadoutUI;
    [SerializeField] CameraChanger camChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void UpdateCanvas()
    {
        if(currentZone == SpaceshipZone.WorldSelect)
        {
            worldSelectLineGO.SetActive(true);
            readyLineGO.SetActive(false);
            loadoutLineGO.SetActive(false);

            WorldselectUI.SetActive(true);
            loadoutUI.SetActive(false);
        }
        else if(currentZone == SpaceshipZone.Loadout)
        {
            worldSelectLineGO.SetActive(false);
            readyLineGO.SetActive(false);
            loadoutLineGO.SetActive(true);

            WorldselectUI.SetActive(false);
            loadoutUI.SetActive(true);
        }
        else
        {
            worldSelectLineGO.SetActive(false);
            readyLineGO.SetActive(true);
            loadoutLineGO.SetActive(false);

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
