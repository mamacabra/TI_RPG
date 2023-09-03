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
    public bool shipIsMoving;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();

        shipIsMoving = false;
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

            Vector3 pos;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            {
                if (raycastHit.collider.gameObject.name != "GroundNavMesh")
                {
                    pos = raycastHit.collider.gameObject.transform.position;
                    bool canNavegate = MapManager.Instance.CheckIndex(raycastHit.collider.gameObject);
                    if (!canNavegate)
                    {
                        CanClick();
                        return;
                    }
                    
                    Vector3 pCam = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
                    MapManager.Instance.MoveCamera(pCam, pos);
                }
                else
                {
                    CanClick();
                    pos = raycastHit.point;
                }
              
                Navegate(pos);
                return;
            }
            CanClick();
        }

        if (shipIsMoving)
        {
            if (!navMeshAgent.hasPath)
            {
                MapManager.Instance.shipArrived = true;
                shipIsMoving = false;
            }
        }
    }

    void Navegate(Vector3 pos)
    {
        MapManager.Instance.shipArrived = false;
        shipIsMoving = true;
        navMeshAgent.SetDestination(pos);
    }

    public void CanClick()
    {
        canClick = true;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Island") && canClick)
        {
            canClick = false;
            other.GetComponent<MeshCollider>().enabled = false;
            MapManager.Instance.CheckIndex(other.gameObject);
            Vector3 pCam = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
            MapManager.Instance.MoveCamera(pCam, other.transform.position);
            
        }
    }*/
}
