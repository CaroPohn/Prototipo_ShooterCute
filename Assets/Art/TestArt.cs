using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    [SerializeField] EggShield[] roots;
    [SerializeField] float timeForGrowth;
    [SerializeField] float timeAfterGrowthToDisappear;
    private void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine("RootsCoroutine");
        }

    }
    IEnumerator RootsCoroutine()
    {
        foreach (EggShield root in roots) 
        {
            root.SetGrowValue(0);
        }
        foreach (EggShield root in roots)
        {
            float timer = 0f;
            while(timer < timeForGrowth)
            {
                root.SetGrowValue(timer / timeForGrowth);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return new WaitForSeconds(timeAfterGrowthToDisappear);
        foreach (EggShield root in roots)
        {
            root.Desintegrate();
        }
    }
}
