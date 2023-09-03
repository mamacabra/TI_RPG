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

    [SerializeField] private Transform cubeLookAt;

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

            Vector3 pos;
            bool island = false;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            {
                MapManager.Instance.shipArrived = false;
                if (raycastHit.collider.gameObject.name != "GroundNavMesh")
                {
                    pos = raycastHit.collider.gameObject.transform.GetChild(0).position;
                    bool canNavegate = MapManager.Instance.CheckIndex(raycastHit.collider.gameObject);
                    island = true;
                    if (!canNavegate)
                    {
                        CanClick();
                        return;
                    }
                }
                else
                {
                    CanClick();
                    pos = raycastHit.point;
                }

                cubeLookAt.position = pos;
                Navegate(pos, island);
                return;
            }
            CanClick();
        }
    }

    void Navegate(Vector3 pos, bool island)
    {
        transform.DOLocalRotate(cubeLookAt.transform.localPosition, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            navMeshAgent.SetDestination(pos);
            StartCoroutine(WaitToCheckIsland());
        });
        
        IEnumerator WaitToCheckIsland()
        {
            yield return new WaitUntil(() => Vector3.Distance(pos, transform.position) <= 0.2f);
            yield return new WaitForSeconds(0.5f);
            MapManager.Instance.shipArrived = true;
            if (island)
            {
                Vector3 pCam = new Vector3(cam.transform.position.x, cam.transform.position.y,
                    cam.transform.position.z);
                MapManager.Instance.MoveCamera(pCam, pos);
            }
        }
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
