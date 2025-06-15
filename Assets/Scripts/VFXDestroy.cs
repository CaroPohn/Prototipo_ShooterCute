using System.Collections;
using UnityEngine;

public class VFXDestroy : MonoBehaviour
{
    public float lifeTime = 5.0f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
