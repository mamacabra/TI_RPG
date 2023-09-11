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
    private bool canClick = true;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();
    }

    private void OnEnable()
    {
        MapManager.Instance.CanClick += CanClick;
    }

    private void OnDisable()
    {
        if (!MapManager.Instance) return;
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
            else
            {
                CanClick();
            }
        }
    }

    void Navegate(Vector3 pos)
    {
        Vector3 p = new Vector3(pos.x + 1, pos.y, pos.z);
        Vector3 pCam = new Vector3(pos.x, cam.transform.position.y, cam.transform.position.z + 2.75f);
        navMeshAgent.SetDestination(p);
        MapManager.Instance.MoveCamera(pCam, pos);

        StartCoroutine(WaitToCheckIsland());

        IEnumerator WaitToCheckIsland()
        {
            yield return new WaitUntil(() => Vector3.Distance(p, navMeshAgent.transform.position) <= 0.2f);
            yield return new WaitForSeconds(0.5f);
            MapManager.Instance.shipArrived = true;
        }
    }

    public void CanClick()
    {
        canClick = true;
    }
}