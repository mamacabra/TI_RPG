using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VFXLine : MonoBehaviour
{
    [SerializeField] private Transform initialPoint;
    private Vector3 curvePoint;
    [SerializeField][Range(-5.0f, 5.0f)] private float curve;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Vector3 targetPointOffset;
    private LineRenderer lineRenderer;
    [SerializeField][Range(2.0f, 100.0f)] private float vertexCount = 12.0f;
    [SerializeField][Tooltip("To set the amount, change the vertex count value. ps: it might look weird if the line is too long")] private bool changeAmountOfArrows = false;
    [SerializeField] private Color color = Color.white;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetColor(color);
    }

    void Update()
    {
        curvePoint = new Vector3((initialPoint.transform.position.x + targetPoint.transform.position.x) / 2, curvePoint.y, (initialPoint.transform.position.z + targetPoint.transform.position.z) / 2);
        curvePoint.y = curve;
        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(initialPoint.position, curvePoint, ratio);
            var tangent2 = Vector3.Lerp(curvePoint, targetPoint.position + targetPointOffset, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());

        if (changeAmountOfArrows)
        {
            lineRenderer.textureMode = LineTextureMode.RepeatPerSegment;
        }
        else
        {
            lineRenderer.textureMode = LineTextureMode.Tile;
        }
    }
    public void SetColor(Color _color)
    {
        // lineRenderer.material.SetColor("_BaseColor", _color);
        // lineRenderer.material.SetColor("_EmissionColor", _color * 2.5f);
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
