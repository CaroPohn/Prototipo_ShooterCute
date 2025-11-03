using UnityEngine;

public class AbilityLocationHandler : MonoBehaviour
{
    [SerializeField] private GameObject bombLumming;
    [SerializeField] private GameObject electricLumming;

    private void Start()
    {
        TurnOnAbilitys();
    }

    private void TurnOnAbilitys()
    {
        bombLumming.SetActive(false);  
        electricLumming.SetActive(false);  
    }
}
