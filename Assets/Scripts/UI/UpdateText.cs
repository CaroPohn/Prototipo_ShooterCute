using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] string nameToDisplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnValidate()
    {
        tmp.text = nameToDisplay;
    }
}
