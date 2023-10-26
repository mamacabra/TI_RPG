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

                Navegate(raycastHit.collider.GetComponent<MapNodeTest>().harbor.position);
            }
            else
            {
                MapManager.Instance.canClick = true;
            }
        }
    }

    void Navegate(Vector3 pos)
    {
       
        Vector3 p = new Vector3(pos.x, pos.y, pos.z);
        Vector3 pCam = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 2.75f);
        navMeshAgent.SetDestination(p);
        MapManager.Instance.MoveCamera(pCam, pos);

        StartCoroutine(WaitToCheckIsland());

        IEnumerator WaitToCheckIsland()
        {
            yield return new WaitUntil(() => Vector3.Distance(p, navMeshAgent.transform.position) <= 0.5f);
            yield return new WaitForSeconds(0.5f);
            Debug.Log("chegou");
            MapManager.Instance.shipArrived = true;
        }
    }
}