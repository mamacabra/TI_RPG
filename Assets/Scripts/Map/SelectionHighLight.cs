using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class SelectionHighLight : MonoBehaviour
{
    public Material highlightMaterialCorretIsland;
    public Material highlightMaterialIncorretIsland;
    public Material selectionMaterial;

    private Transform highlightTransform;
    private MeshRenderer highlight;
    private MeshRenderer lastHighlight;
    private RaycastHit raycastHit;

    [SerializeField] LayerMask _layerMask;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue,
                _layerMask) && MapManager.Instance.canClick) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlightTransform = raycastHit.transform;
            if (highlightTransform.CompareTag("Island"))
            {
                highlight = raycastHit.transform.GetComponent<MapNodeTest>().meshRenderer;
                if (lastHighlight != null)
                {
                    if (lastHighlight.material != selectionMaterial)
                    {
                        lastHighlight.material = selectionMaterial;
                    }
                }

                if (highlight.material != highlightMaterialCorretIsland)
                {
                    bool correct = MapManager.Instance.CheckIndexHighlight(highlightTransform.gameObject);
                    if(correct)
                        highlight.material = highlightMaterialCorretIsland;
                    else
                        highlight.material = highlightMaterialIncorretIsland;

                    lastHighlight = highlight;
                }
            }
        }
        else
        {
            if (lastHighlight == null) return;
            if (lastHighlight.material != selectionMaterial)
            {
                lastHighlight.material = selectionMaterial;
            }
        }

    }
}
