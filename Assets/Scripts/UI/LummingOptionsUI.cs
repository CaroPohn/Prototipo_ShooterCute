using UnityEngine;

public class LummingOptionsUI : MonoBehaviour
{
    [SerializeField] GameObject container;
    public void ShowOptions()
    {
        container.SetActive(true);
    }
    public void HideOptions()
    {
        container.SetActive(false);
    }
}
