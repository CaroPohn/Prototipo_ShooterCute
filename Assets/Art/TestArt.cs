using UnityEngine;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Image image;
    [SerializeField] float cooldown = 3;
    float fill = 1f;
    bool onCooldown = false;
    float timer = 0;

    [SerializeField] ObjectiveUI objectiveUIScript;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (anim.GetBool("isReady"))
            {
                fill = 0f;
                timer = 0;
                onCooldown = true;
            }
            else 
            {
                fill = 1f;
                onCooldown = false;
            }
            anim.SetBool("isReady",!anim.GetBool("isReady"));
        }
        if (!anim.GetBool("isReady"))
        {
            timer += Time.deltaTime;
            fill = timer/cooldown;
        }
        image.fillAmount = Mathf.Clamp(fill,0,1);

        if (Input.GetKeyUp(KeyCode.A))
        {
            objectiveUIScript.ShowNewMission("FIND THE EGG", "Locate the endangered Lumming egg");

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            objectiveUIScript.ShowNewMission("SURVIVE", "Defend yourself against the Gnutorrs!");

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            objectiveUIScript.HideMissionNotification();

        }
    }
}
