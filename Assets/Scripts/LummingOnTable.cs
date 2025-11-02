using UnityEngine;

public class LummingOnTable : MonoBehaviour
{
    [SerializeField] GameObject bombGO;
    [SerializeField] GameObject chispeanGO;
    public void UpdateLummingOnTable(Lumming lumming)
    {
        if(lumming == Lumming.Bomb)
        {
            chispeanGO.SetActive(false);
            bombGO.SetActive(true);

        }
        else if(lumming == Lumming.Chispean)
        {
            chispeanGO.SetActive(true);
            bombGO.SetActive(false);
        }
        else
        {
            chispeanGO.SetActive(false);
            bombGO.SetActive(false);

        }
    }
}
