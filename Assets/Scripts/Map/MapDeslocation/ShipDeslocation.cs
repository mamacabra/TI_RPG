using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

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
                Navegate(raycastHit.point);
            }
           
            CanClick();
            
        }
    }

    void Navegate(Vector3 pos)
    {
        
        navMeshAgent.SetDestination(pos);
    }

    public void CanClick()
    {
        canClick = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Island") && canClick)
        {
            canClick = false;
            other.GetComponent<MeshCollider>().enabled = false;
            MapManager.Instance.CheckIndex(other.gameObject);
            Vector3 pCam = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
            MapManager.Instance.MoveCamera(pCam, other.transform.position);
            
        }
    }
}
