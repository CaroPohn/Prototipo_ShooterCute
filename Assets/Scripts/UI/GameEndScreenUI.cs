using UnityEngine;

public class GameEndScreenUI : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject containerGO;
    
    public void PlayMissionFailedAnimation()
    {
        containerGO.SetActive(true);
        anim.SetTrigger("Failed");
    }
    public void PlayMissionAccomplishedAnimation()
    {
        containerGO.SetActive(true);
        anim.SetTrigger("Accomplished");
    }
}
