using UnityEngine;

public class GameEndScreenUI : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject containerGO;
    [SerializeField] GameObject missionFailedContainer;
    [SerializeField] GameObject missionAccomplishedContainer;

    public void PlayMissionFailedAnimation()
    {
        containerGO.SetActive(true);
        anim.SetTrigger("Failed");
        missionFailedContainer.SetActive(true);
    }
    public void PlayMissionAccomplishedAnimation()
    {
        containerGO.SetActive(true);
        anim.SetTrigger("Accomplished");
        missionAccomplishedContainer.SetActive(true);
    }
}
