using UnityEngine;

public class LummingOnTable : MonoBehaviour
{
    [SerializeField] GameObject bombGO;
    [SerializeField] GameObject chispeanGO;
    [SerializeField] Renderer gunRenderer;
    [SerializeField] Material bombMat;
    [SerializeField] Material chispeanMat;
    [SerializeField] Material gunDefaultMaterial;
    public void UpdateLummingOnTable(Lumming lumming)
    {
        if(lumming == Lumming.Bomb)
        {
            chispeanGO.SetActive(false);
            bombGO.SetActive(true);
            gunRenderer.material = bombMat;

        }
        else if(lumming == Lumming.Chispean)
        {
            chispeanGO.SetActive(true);
            bombGO.SetActive(false);
            gunRenderer.material = chispeanMat;
        }
        else
        {
            chispeanGO.SetActive(false);
            bombGO.SetActive(false);
            gunRenderer.material = gunDefaultMaterial;

        }
    }
}
