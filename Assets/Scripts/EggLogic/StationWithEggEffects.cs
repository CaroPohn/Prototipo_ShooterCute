using UnityEngine;

public class StationWithEggEffects : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Close()
    {
        animator.SetTrigger("Close");
    }
    public void Die()
    {
        animator.SetTrigger("Die");
    }
}
