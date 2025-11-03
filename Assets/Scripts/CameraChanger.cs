using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] Transform LevelSelectorCameraTransform;
    [SerializeField] Transform LoadoutCameraTransform;
    [SerializeField] Transform SummaryCameraTransform;
    [SerializeField] Transform LookingAtDoorCameraTransform;

    public void ChangeCameraTo(SpaceshipZone zone)
    {
        Transform transformToChangeTo = null;
        if(zone == SpaceshipZone.Ready)
        {
            transformToChangeTo = SummaryCameraTransform;
        }
        else if(zone == SpaceshipZone.WorldSelect)
        {
            transformToChangeTo = LevelSelectorCameraTransform;
        }
        else if (zone == SpaceshipZone.Loadout)
        {
            transformToChangeTo = LoadoutCameraTransform;
        }
        else
        {
            transformToChangeTo = LookingAtDoorCameraTransform;
        }

        Camera.main.transform.position = transformToChangeTo.position;
        Camera.main.transform.rotation = transformToChangeTo.rotation;
    }
}
