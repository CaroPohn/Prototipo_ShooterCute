using UnityEngine;

public class ZoneChangerUI : MonoBehaviour
{
    [SerializeField] GameObject worldSelectLineGO;
    [SerializeField] GameObject readyLineGO;
    [SerializeField] GameObject loadoutLineGO;
    public void UpdateZoneChanger(SpaceshipZone zone)
    {
        if (zone == SpaceshipZone.WorldSelect)
        {
            worldSelectLineGO.SetActive(true);
            readyLineGO.SetActive(false);
            loadoutLineGO.SetActive(false);
        }
        else if (zone == SpaceshipZone.Loadout)
        {
            worldSelectLineGO.SetActive(false);
            readyLineGO.SetActive(false);
            loadoutLineGO.SetActive(true);
        }
        else if(zone == SpaceshipZone.Ready)
        {
            worldSelectLineGO.SetActive(false);
            readyLineGO.SetActive(true);
            loadoutLineGO.SetActive(false);
        }
    }
}
