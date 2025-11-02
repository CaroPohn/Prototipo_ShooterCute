using UnityEngine;

public class CurrentPlanetInformation : MonoBehaviour
{
    [SerializeField] GameObject LavaWorldData;
    [SerializeField] GameObject unknownWorldData;
    public void UpdateDataToPlanet(World world)
    {
        if(world == World.Lava)
        {
            LavaWorldData.SetActive(true);
            unknownWorldData.SetActive(false);
        }
        else
        {
            LavaWorldData.SetActive(false);
            unknownWorldData.SetActive(true);
        }
    }
}
