using UnityEngine;

public class LummingOnTable : MonoBehaviour
{
    [SerializeField] GameObject[] lummingsGO;
    public void UpdateLummingOnTable(Lumming lumming)
    {
        int indexToShow = (int)lumming;
        for (int i = 1; i < lummingsGO.Length; i++) 
        {
            if (i == indexToShow) lummingsGO[i].gameObject.SetActive(true);
            else lummingsGO[i].SetActive(false);
        }
    }
}
