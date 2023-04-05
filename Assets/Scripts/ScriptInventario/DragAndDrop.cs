using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 initialPosition;
    private Collider dropZoneCollider;
    private RectTransform rectTransform;

    void Start()
    {
        dropZoneCollider = GameObject.FindGameObjectWithTag("DropZone").GetComponent<Collider>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPos = rectTransform.anchoredPosition + eventData.delta / transform.parent.GetComponent<Canvas>().scaleFactor;

        float halfWidth = rectTransform.rect.width / 2f;
        float halfHeight = rectTransform.rect.height / 2f;
        float xMax = Screen.width / 2f - halfWidth;
        float xMin = -xMax;
        float yMax = Screen.height / 2f - halfHeight;
        float yMin = -yMax;

        newPos.x = Mathf.Clamp(newPos.x, xMin, xMax);
        newPos.y = Mathf.Clamp(newPos.y, yMin, yMax);

        rectTransform.anchoredPosition = newPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dropZoneCollider.bounds.Intersects(new Bounds(rectTransform.position, rectTransform.sizeDelta)))
        {
            Debug.Log("Object dropped in the drop zone!");
        }
        else
        {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}