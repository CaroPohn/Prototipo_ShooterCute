using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] Image fillImage;


    void UpdateFillAmount(float amount)
    {
        fillImage.fillAmount = amount;
    }
    void SetReady()
    {

    }
    void StartCooldown()
    {

    }
}
