using UnityEngine;
using System.Collections;

public class RootSequenceManager : MonoBehaviour
{
    [SerializeField] EggShield[] roots;
    [SerializeField] float timeForGrowth;

    public void Start()
    {
        //GrowRootOneByOne();
    }
    public void GrowRootOneByOne()
    {
        StartCoroutine(RootsInOrderCoroutine());
    }
    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space)) 
        {
            GrowRootOneByOne();
        }*/
    }
    public void DesintegrateAllRoots()
    {
        StopAllCoroutines();
        foreach(EggShield root in roots)
        {
            root.Desintegrate();
        }
    }
    IEnumerator RootsInOrderCoroutine()
    {
        foreach (EggShield root in roots)
        {
            root.SetGrowValue(0);
        }
        foreach (EggShield root in roots)
        {
            float timer = 0f;
            while (timer < timeForGrowth)
            {
                if(root.IsInverted) root.SetGrowValue(1f - timer / timeForGrowth);
                else root.SetGrowValue(timer / timeForGrowth);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
