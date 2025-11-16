using UnityEngine;
using UnityEngine.UI;

public class InteractHUD_UI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Image lineImage;

    private void Start()
    {
        lineImage.material = Instantiate(lineImage.material);
    }

    public void Appear()
    {
        animator.SetBool("visible", true);
    }
    public void Hide()
    {
        animator.SetBool("visible", false);
    }
}
