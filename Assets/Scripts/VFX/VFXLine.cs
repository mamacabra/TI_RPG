using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;
using UnityEngine;
using System;

public class VFXLine : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform initialPoint;
    [SerializeField] private Vector2 initialPointOffset = new Vector2(0.0f, -2.0f);
    private Vector2 curvePoint;
    [SerializeField][Range(0, 1080.0f)] private float curve;
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
        if (initialPoint && targetPoint)
        {
            Vector3 initialPWorld = m_Camera.WorldToScreenPoint(initialPoint.position);
            Vector3 targetPWorld = m_Camera.WorldToScreenPoint(targetPoint.position);
            curvePoint = new Vector2((initialPWorld.x + targetPWorld.x) / 2, (initialPWorld.y + targetPWorld.y) / 2);
            curvePoint.y = curve;
            var pointList = new List<Vector2>();

            for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
            {
                var tangent1 = Vector2.Lerp((Vector2)initialPWorld + initialPointOffset, curvePoint, ratio);
                var tangent2 = Vector2.Lerp(curvePoint, (Vector2)targetPWorld + targetPointOffset, ratio);
                var curve = Vector2.Lerp(tangent1, tangent2, ratio);

                pointList.Add(curve);
            }

            lineRenderer.Points = new Vector2[pointList.Count];
            lineRenderer.Points = pointList.ToArray();
        }
        else if (mousePos && initialPoint)
        {
            Vector3 mousePosition = Vector3.zero;
            mousePosition = Input.mousePosition;
            Vector3 worldSpaceToCanvas = m_Camera.WorldToScreenPoint(initialPoint.position);

            curvePoint = new Vector2((worldSpaceToCanvas.x + mousePosition.x) / 2, (worldSpaceToCanvas.y + mousePosition.y) / 2);
            curvePoint.y = curve;
            var pointList = new List<Vector2>();

            for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
            {
                var tangent1 = Vector2.Lerp((Vector2)worldSpaceToCanvas + initialPointOffset, curvePoint, ratio);
                var tangent2 = Vector2.Lerp(curvePoint, (Vector2)mousePosition + targetPointOffset, ratio);
                var curve = Vector2.Lerp(tangent1, tangent2, ratio);

                pointList.Add(curve);
            }

            lineRenderer.Points = new Vector2[pointList.Count];
            lineRenderer.Points = pointList.ToArray();

        }
        lineRenderer.material.mainTextureOffset = new Vector2(-Time.time - (float)Math.Truncate(Time.time), 0);
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
