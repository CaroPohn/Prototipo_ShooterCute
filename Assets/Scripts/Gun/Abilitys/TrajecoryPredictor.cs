using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    public Transform origin;               // punto de salida del proyectil
    public LineRenderer lineRenderer;
    public LayerMask collisionMask = ~0;   // capas a considerar
    public float timeStep = 0.05f;         // tamaño del paso de simulación (seg)
    public int maxSteps = 300;             // tope de pasos (evita loops infinitos)
    public float projectileRadius = 0.1f;  // radio para SphereCast (0 para Raycast)
    public bool useGravity = true;
    public float gravityScale = 1f;        // multiplica Physics.gravity
    public GameObject impactMarkerPrefab;   // opcional: prefab a instanciar en el punto de impacto
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

    // Llama a este método con la velocidad inicial (por ejemplo: moveDirection)
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
            // integrar una pequeña fracción de tiempo
            vel += gravity * timeStep;
            pos += vel * timeStep;

            // comprobar colisión entre lastPos y pos
            RaycastHit hit;
            Vector3 dir = pos - lastPos;
            float dist = dir.magnitude;
            if (dist > 0f)
            {
                dir /= dist; // normalizar

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
                    // agregar punto de impacto y terminar
                    points.Add(hit.point);
                    PlaceImpactMarker(hit.point, hit.normal);
                    return points;
                }
            }

            points.Add(pos);
            lastPos = pos;
        }

        // si no colisionó, remover impact marker si existe
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
        currentImpactMarker.transform.rotation = Quaternion.LookRotation(normal); // orienta según normal
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
