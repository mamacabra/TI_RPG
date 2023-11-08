using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ShipDeslocation : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    private NavMeshAgent navMeshAgent;
    private Camera cam;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && MapManager.Instance.canClick)
        {
            MapManager.Instance.canClick = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            {
                bool canNavegate = MapManager.Instance.CheckIndex(raycastHit.collider.gameObject);
                if (!canNavegate)
                {
                    MapManager.Instance.canClick = true;
                    return;
                }

                Navegate(raycastHit.collider.transform.position,raycastHit.collider.GetComponent<MapNodeTest>().harbor.position);
            }
            else
            {
                MapManager.Instance.canClick = true;
            }
        }
    }

    void Navegate(Vector3 islandPos,Vector3 harborPos)
    {
        Vector3 p = harborPos;
        Vector3 pCam = new Vector3(islandPos.x, cam.transform.position.y, islandPos.z - 5f);
        Vector3 pCam2 = new Vector3(islandPos.x,islandPos.y, islandPos.z);
        navMeshAgent.SetDestination(p);
        MapManager.Instance.MoveCamera(pCam, pCam2);

        StartCoroutine(WaitToCheckIsland());

        IEnumerator WaitToCheckIsland()
        {
            yield return new WaitUntil(() => Vector3.Distance(p, navMeshAgent.transform.position) <= 0.5f);
            yield return new WaitForSeconds(0.5f);
            MapManager.Instance.shipArrived = true;
        }
    }
}