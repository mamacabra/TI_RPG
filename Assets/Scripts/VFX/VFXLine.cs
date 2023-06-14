using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;
using UnityEngine;
using Unity.Mathematics;

public class VFXLine : MonoBehaviour
{
    [SerializeField] private Transform initialPoint;
    [SerializeField] private Vector2 initialPointOffset = new Vector2(0.0f, -2.0f);
    private Vector2 curvePoint;
    [SerializeField][Range(-5.0f, 5.0f)] private float curve;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Vector2 targetPointOffset = new Vector2(0.0f, -2.0f);
    private UILineRenderer lineRenderer;
    [SerializeField][Range(2.0f, 100.0f)] private float vertexCount = 12.0f;
    [SerializeField][Tooltip("To set the amount, change the vertex count value. ps: it might look weird if the line is too long")] private bool changeAmountOfArrows = false;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private bool mousePos = false;

    Plane plane = new Plane(Vector3.back, 0);

    private void Awake()
    {
        lineRenderer = GetComponent<UILineRenderer>();
        SetColor(color);
    }

    void Update()
    {
        if (initialPoint != null && targetPoint != null)
        {
            curvePoint = new Vector2((initialPoint.transform.position.x + targetPoint.transform.position.x) / 2, (initialPoint.transform.position.y + targetPoint.transform.position.y) / 2);
            curvePoint.y = curve;
            var pointList = new List<Vector2>();

            for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
            {
                var tangent1 = Vector2.Lerp((Vector2)initialPoint.position + initialPointOffset, curvePoint, ratio);
                var tangent2 = Vector2.Lerp(curvePoint, (Vector2)targetPoint.position + targetPointOffset, ratio);
                var curve = Vector2.Lerp(tangent1, tangent2, ratio);

                pointList.Add(curve);
            }

            lineRenderer.Points = new Vector2[pointList.Count];
            lineRenderer.Points = pointList.ToArray();

            // if (changeAmountOfArrows)
            // {
            //     lineRenderer.textureMode = LineTextureMode.RepeatPerSegment;
            // }
            // else
            // {
            //     lineRenderer.textureMode = LineTextureMode.Tile;
            // }
        }
        else if (mousePos)
        {
            Vector3 mousePosition = Vector3.zero;
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                mousePosition = ray.GetPoint(distance);
            }
            curvePoint = new Vector2((initialPoint.transform.position.x + mousePosition.x) / 2, (initialPoint.transform.position.y + mousePosition.y) / 2);
            curvePoint.y = curve;
            var pointList = new List<Vector2>();

            for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
            {
                var tangent1 = Vector2.Lerp((Vector2)initialPoint.position + initialPointOffset, curvePoint, ratio);
                var tangent2 = Vector2.Lerp(curvePoint, (Vector2)mousePosition + targetPointOffset, ratio);
                var curve = Vector2.Lerp(tangent1, tangent2, ratio);

                pointList.Add(curve);
            }

            lineRenderer.Points = new Vector2[pointList.Count];
            lineRenderer.Points = pointList.ToArray();

            lineRenderer.material.mainTextureOffset = new Vector2(math.frac(-Time.time), 0);
        }
    }
    public void SetColor(Color _color)
    {
        lineRenderer.material.SetColor("_Color", _color * 2.5f);
    }

    public void SetTargetPoint(Transform _target)
    {
        if (_target != null)
        {
            targetPoint = _target;
        }
    }
    public void SetInitialPoint(Transform _initial)
    {
        if (_initial != null)
        {
            initialPoint = _initial;
        }
    }
}
