using System.Collections;
using UnityEngine;

public class SpaceshipZoneSelectorUI : MonoBehaviour
{
    [SerializeField] SpaceshipZone[] spaceshipZonesInOrder;
    SpaceshipZone currentZone = SpaceshipZone.WorldSelect;
    int index = 0;
    [SerializeField] GameObject WorldselectUI;
    [SerializeField] GameObject loadoutUI;
    [SerializeField] SummaryUI summaryUI;
    [SerializeField] float timeToMoveBetweenZones = 2;
    bool moving = false;
    [SerializeField] PointLineUI left_pointLineUI;
    [SerializeField] PointLineUI right_pointLineUI;

    [SerializeField] SpaceShipZoneScreen[] spaceShipZones;
    [SerializeField] ZoneUI[] zoneMarkers;
    [SerializeField] PointLineUI[] pointLineUIs;
    [SerializeField] CameraChanger cameraChanger;
    int currentZoneIndex = 0;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1.0f;
        
    }

    private void Update()
    {
        MoveWithInput();
    }
    private void Start()
    {
        zoneMarkers[0].SelectThisZone();
        spaceShipZones[0].ShowAllScreens();
    }


    void TurnOffAllScreens()
    {
        foreach(SpaceShipZoneScreen screen in spaceShipZones) 
        {
            screen.HideAllScreens();
        }

    }
    private void MoveWithInput()
    {
        if (moving) return;
        if (Input.GetKeyDown(KeyCode.Q))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.E))
            MoveRight();
    }

    public void MoveLeft()
    {
        if(moving) return;
        if (currentZoneIndex == 0) return;
        StopAllCoroutines();
        StartCoroutine(MoveZoneCoroutine(false));
    }

    public void MoveRight()
    {
        if (moving) return;
        if (currentZoneIndex >= spaceShipZones.Length) return;
        StopAllCoroutines();
        StartCoroutine(MoveZoneCoroutine(true));
    }

    
    IEnumerator MoveZoneCoroutine(bool movingRight)
    {
        moving = true;
        int direction = 1;
        if(!movingRight) direction  = -1;
        spaceShipZones[currentZoneIndex].HideAllScreens();
        if (movingRight)
        {
            zoneMarkers[currentZoneIndex].LeaveThisZone();
        }
        else
        {
            zoneMarkers[currentZoneIndex].BlockThisZone();
        }
        
        float timer = 0f;
        while(timer < timeToMoveBetweenZones)
        {
            timer += Time.deltaTime;
            float progress = timer / timeToMoveBetweenZones;
            if(movingRight) pointLineUIs[currentZoneIndex].SetProgress(progress);
            else pointLineUIs[currentZoneIndex-1].SetProgress(1 - progress);

            cameraChanger.TransitionToCamera(currentZoneIndex + direction,progress);
            yield return null;
        }
        if (movingRight) currentZoneIndex++;
        else currentZoneIndex--;
        spaceShipZones[currentZoneIndex].ShowAllScreens();
        zoneMarkers[(currentZoneIndex)].SelectThisZone();
        moving = false;
    }
}
