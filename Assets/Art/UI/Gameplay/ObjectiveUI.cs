using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] Animator objectiveAnim;
    [SerializeField] TextWriter textWriterScript;
    public void ShowNewMission(string missionTitle, string missionDescription)
    {
        objectiveAnim.SetBool("visible", true);
        textWriterScript.title = missionTitle;
        textWriterScript.description = missionDescription;
    }
    public void HideMissionNotification()
    {
        objectiveAnim.SetBool("visible", false);
    }
}
