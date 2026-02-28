using UnityEngine;

public class ZoneChangerUI : MonoBehaviour
{
    [SerializeField] ZoneUI locationZone;
    [SerializeField] ZoneUI loadoutZone;
    [SerializeField] ZoneUI readyZone;

    SpaceshipZone currentZone = SpaceshipZone.WorldSelect;

    public void GoToLoadout()
    {

    }

}
