using TMPro;
using UnityEngine;

public class LoadoutWarningsUI : MonoBehaviour
{
    [SerializeField] GameObject containerGO;
    [SerializeField] TextMeshProUGUI TextMeshProUGUI;
    public void SameLummingWarning()
    {
        TextMeshProUGUI.text = "Weapon and ability cannot be the same lumming";
        containerGO.SetActive(true);
    }
    public void EmptySlotWarning()
    {
        TextMeshProUGUI.text = "Weapon and ability must have a lumming assigned";
        containerGO.SetActive(true)
    }
    public void CloseButton()
    {
        containerGO.SetActive(false);
    }
}
