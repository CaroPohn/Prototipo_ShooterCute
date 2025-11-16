using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float glowAmount;
    [SerializeField] private TextMeshProUGUI tmpUI;

    private void Start()
    {
    }

    public void OnHover()
    {
        anim.SetBool("hovering", true);
    }
    public void OnUnhover()
    {
        anim.SetBool("hovering", false);
    }

    public void DebugClick()
    {
        Debug.Log("CLICK");
    }
    void UpdateGlow()
    {
        tmpUI.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowAmount);
    }
    private void OnValidate()
    {
        UpdateGlow();
    }
    private void LateUpdate()
    {
        UpdateGlow();
    }
}
