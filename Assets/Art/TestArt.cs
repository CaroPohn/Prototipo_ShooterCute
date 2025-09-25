using UnityEngine;

public class TestArt : MonoBehaviour
{
    [SerializeField] HitEffectController hitEffectController;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            hitEffectController.GetHit();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            hitEffectController.Dissolve();
        }
    }
}
