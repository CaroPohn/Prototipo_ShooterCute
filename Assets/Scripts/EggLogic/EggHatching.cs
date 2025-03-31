using UnityEngine;

public class EggHatching : MonoBehaviour
{
    [SerializeField] private PickAndDrop pickDropPlayer;

    private float maxTime = 10f;
    private float currentTime;

    private bool isBeingHeld;
    private bool isMeshChanged;

    [SerializeField] private Mesh cubeMesh;
    private MeshFilter meshFilter;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        currentTime = maxTime;

        isBeingHeld = false;
        isMeshChanged = false;
    }

    private void OnEnable()
    {
        if (pickDropPlayer != null)
        {
            pickDropPlayer.OnEggGrabbed += EggGrab;
            pickDropPlayer.OnEggDropped += EggDrop;
        }
    }

    private void OnDisable()
    {
        if (pickDropPlayer != null)
        {
            pickDropPlayer.OnEggGrabbed -= EggGrab;
            pickDropPlayer.OnEggDropped -= EggDrop;
        }
    }

    private void Update()
    {
        if (isBeingHeld && currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }
        else if(!isBeingHeld && currentTime <= 0f)
        {
            if (!isMeshChanged)
            {
                if (meshFilter != null && cubeMesh != null)
                {
                    meshFilter.mesh = cubeMesh;
                    isMeshChanged = true;
                }
                else
                {
                    Debug.LogWarning("MeshFilter o la malla del cubo no está asignada.");
                }
            }
        }
    }

    private void EggGrab(GameObject sender)
    {
        if (sender == gameObject)
        {
            isBeingHeld = true;
        }
    }

    private void EggDrop(GameObject sender)
    {
        if (sender == gameObject)
        {
            isBeingHeld = false;
        }
    }
}
