using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ready_Button_Spaceship : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] Button button;
    [SerializeField] Sprite ReadySprite;
    [SerializeField] Sprite DisabledSprite;
    [SerializeField] Color readyColor;
    [SerializeField] Color disabledColor;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] string readyText;
    [SerializeField] string disabledText;
    [SerializeField] GameObject glowGO;
    [SerializeField] ButtonState initialButtonState = ButtonState.Ready;
    
    public void PlayAnimationReady()
    {
        buttonImage.sprite = ReadySprite;
        glowGO.SetActive(true);
        tmp.color = readyColor;
        tmp.text = readyText;
        button.interactable = true;
    }
    public void PlayAnimationDisabled()
    {
        buttonImage.sprite = DisabledSprite;
        glowGO.SetActive(false);
        tmp.color = disabledColor;
        tmp.text = disabledText;
        button.interactable = false;
    }
    private void OnValidate()
    {
        if(initialButtonState == ButtonState.Ready)
        {
            PlayAnimationReady();
        }
        else
        {
            PlayAnimationDisabled();
        }
    }
}
public enum ButtonState
{
    Ready, // Default value 0
    Disabled, // Default value 1
}
