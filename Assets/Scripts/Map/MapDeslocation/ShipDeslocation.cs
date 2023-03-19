using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShipDeslocation : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    private bool canClick = true;

    private void OnEnable()
    {
        MapManager.Instance.CanClick += CanClick;
    }

    private void OnDisable()
    {
        if(!MapManager.Instance) return;
        MapManager.Instance.CanClick -= CanClick;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            canClick = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            {
                bool canNavegate = MapManager.Instance.CheckIndex(raycastHit.collider.gameObject);
                if (!canNavegate)
                {
                    CanClick();                 
                    return;
                }
                Navegate(raycastHit.collider.gameObject.transform.position);
            }
        }
    }
    

    void Navegate(Vector3 pos)
    {
        Debug.Log("a");
        transform.DOMove(pos, 2).OnComplete(CheckInsland);
    }

    void CheckInsland()
    {
        MapManager.Instance.CheckIsland();
    }
    
    public void CanClick()
    {
        canClick = true;
    }
}
