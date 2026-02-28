using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseButton : MonoBehaviour
{
    [SerializeField] GameObject GlowGO;
    [SerializeField] Button button;
    [SerializeField] Color interactableTextColor;
    [SerializeField] Color nonInteractableTextColor;
    [SerializeField] TextMeshProUGUI tmp;
    public void Enable()
    {
        button.interactable = true;
        GlowGO.SetActive(true);
        tmp.color = interactableTextColor;
    }
    public void Disable()
    {
        button.interactable = false;
        GlowGO.SetActive(false);
        tmp.color = nonInteractableTextColor;
    }
    public void Clicked()
    {

    }
}
