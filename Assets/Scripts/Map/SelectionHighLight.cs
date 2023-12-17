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
                Material[] materials = highlight.materials;
                if (lastHighlight != null)
                {
                    if (lastHighlight.materials[1] != selectionMaterial)
                    {
                        materials[1] = selectionMaterial;
                        lastHighlight.materials = materials;
                    }
                }

                if (highlight.materials[1] != highlightMaterialCorretIsland)
                {
                    bool correct = MapManager.Instance.CheckIndexHighlight(highlightTransform.gameObject);
                    if(correct){
                        materials[1] = highlightMaterialCorretIsland;
                        highlight.materials = materials;
                    }
                    else{
                        materials[1] = highlightMaterialIncorretIsland;
                        highlight.materials = materials;
                    }

                    lastHighlight = highlight;
                }
            }
        }
        else
        {
            if (lastHighlight == null) return;
            if (lastHighlight.materials[1] != selectionMaterial)
            {
                Material[] materials = lastHighlight.materials;
                materials[1] = selectionMaterial;
                lastHighlight.materials = materials;
            }
        }

    }
}
