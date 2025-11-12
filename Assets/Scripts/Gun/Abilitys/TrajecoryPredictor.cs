using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    public Transform origin;               
    public LineRenderer lineRenderer;
    public LayerMask collisionMask = ~0;   
    public float timeStep = 0.05f;         
    public int maxSteps = 300;             
    public float projectileRadius = 0.1f;  
    public bool useGravity = true;
    public float gravityScale = 1f;        
    public GameObject impactMarkerPrefab;   
    GameObject currentImpactMarker;
    private bool isOn = true;

    void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer) lineRenderer.positionCount = 0;
    }

    void Awake()
    {
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetOrigin(Transform origin)
    {
        this.origin = origin;
    }
    public void SetImpactMarkerPrefab(GameObject prefab)
    {
        impactMarkerPrefab = prefab;
    }
    public void TurnOn()
    {
        isOn = true;
        lineRenderer.enabled = true;
    }
    public void TurnOff()
    {
        isOn = false;
        lineRenderer.enabled = false;
    }

    public void ShowTrajectory(Vector3 initialVelocity)
    {
        if(!isOn) return;

        if (origin == null)
        {
            Debug.LogWarning("TrajectoryPreview: origin no asignado.");
            return;
        }

        List<Vector3> pts = CalculateTrajectoryPoints(origin.position, initialVelocity);
        DrawTrajectory(pts);
    }

    List<Vector3> CalculateTrajectoryPoints(Vector3 startPos, Vector3 initialVelocity)
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 pos = startPos;
        Vector3 vel = initialVelocity;
        Vector3 gravity = useGravity ? Physics.gravity * gravityScale : Vector3.zero;

        points.Add(pos);

        Vector3 lastPos = pos;

        for (int i = 0; i < maxSteps; i++)
        {
            vel += gravity * timeStep;
            pos += vel * timeStep;

            RaycastHit hit;
            Vector3 dir = pos - lastPos;
            float dist = dir.magnitude;
            if (dist > 0f)
            {
                dir /= dist; 

                bool collided;
                if (projectileRadius > 0f)
                {
                    collided = Physics.SphereCast(lastPos, projectileRadius, dir, out hit, dist, collisionMask, QueryTriggerInteraction.Ignore);
                }
                else
                {
                    collided = Physics.Raycast(lastPos, dir, out hit, dist, collisionMask, QueryTriggerInteraction.Ignore);
                }

                if (collided)
                {
                    points.Add(hit.point);
                    PlaceImpactMarker(hit.point, hit.normal);
                    return points;
                }
            }

            points.Add(pos);
            lastPos = pos;
        }

        RemoveImpactMarker();
        return points;
    }

    void DrawTrajectory(List<Vector3> pts)
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = pts.Count;
        lineRenderer.SetPositions(pts.ToArray());
    }

    void PlaceImpactMarker(Vector3 position, Vector3 normal)
    {
        if (impactMarkerPrefab == null) return;

        if (currentImpactMarker == null)
            currentImpactMarker = Instantiate(impactMarkerPrefab);

        currentImpactMarker.transform.position = position;
        currentImpactMarker.transform.rotation = Quaternion.LookRotation(normal);
    }

    void RemoveImpactMarker()
    {
        if (currentImpactMarker != null)
        {
            Destroy(currentImpactMarker.gameObject);
            currentImpactMarker = null;
        }
    }
}
