using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LineDrawDetector : MonoBehaviour
{
    [Header("Cone Settings")]
    [SerializeField] float coneSize;
    [SerializeField] float distance;
    [SerializeField] int segments;
    [Range(0.0f,1.0f)]
    [SerializeField] float lineSize;

    [SerializeField]LineRenderer lineRenderer;
    [SerializeField]Turret_Detector turret_Detector;
    void Start()
    {
        turret_Detector = GetComponentInParent<Turret_Detector>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        distance = turret_Detector.rangeDistance;
        coneSize = turret_Detector.coneSize;
        lineRenderer.startWidth = lineSize;
        lineRenderer.positionCount = segments + 2;

        lineRenderer.SetPosition(0, transform.position);

        float angleStep = (coneSize * 2) / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = -coneSize + (angleStep * i);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            lineRenderer.SetPosition(i + 1, transform.position + direction.normalized * distance);
        }
    }
}
