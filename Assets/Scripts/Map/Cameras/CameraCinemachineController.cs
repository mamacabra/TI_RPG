using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraCinemachineController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraShowStart;
    [SerializeField]private CinemachineVirtualCamera cameraDeslocate;
    [SerializeField]private CinemachineVirtualCamera cameraZoomIsland;
    private Vector3 currentIslandPos;
    private void OnEnable()
    {
       
        MapManager.Instance.ShowMap += ShowMap;
        MapManager.Instance.MoveCameraDeslocate += MoveCamera;
        MapManager.Instance.ZoomCameraIsland += ZoomCamera;
        MapManager.Instance.ResetCameras += Reset;
    }
    private void OnDisable()
    {
        if(!MapManager.Instance) return;
        MapManager.Instance.ShowMap -= ShowMap;
        MapManager.Instance.MoveCameraDeslocate -= MoveCamera;
        MapManager.Instance.ZoomCameraIsland -= ZoomCamera;
        MapManager.Instance.ResetCameras -= Reset;
    }

    public void ShowMap(bool state)
    {
        cameraShowStart.gameObject.SetActive(state);
        cameraDeslocate.gameObject.SetActive(!state);
    }
    void MoveCamera(Vector3 pos, Vector3 islandPos)
    {
        currentIslandPos = islandPos;
        cameraDeslocate.gameObject.SetActive(true);
        cameraZoomIsland.gameObject.SetActive(false);
        cameraDeslocate.transform.DOMove(pos,1.25f).SetEase(Ease.Linear).OnComplete( ()=>
        {
            StartCoroutine(WaitToCheckIsland());
        });
        IEnumerator WaitToCheckIsland()
        {
            yield return new WaitUntil(() =>  MapManager.Instance.shipArrived);
            yield return new WaitForSeconds(0.1f);
            //Botar o fade aqui ou durante o "checkIsland"
            MapManager.Instance.CheckIsland();
        }
    }
    void ZoomCamera()
    {
        cameraZoomIsland.transform.position = cameraDeslocate.transform.position;
        cameraDeslocate.gameObject.SetActive(false);
        cameraZoomIsland.gameObject.SetActive(true);
        cameraZoomIsland.transform.DOMove(currentIslandPos, 1).OnComplete(()=>MapManager.Instance.zoomCamOver = true);
    }
    void Reset()
    {
        cameraDeslocate.gameObject.SetActive(true);
        cameraZoomIsland.gameObject.SetActive(false);
        cameraZoomIsland.transform.position = cameraDeslocate.transform.position;
    }
}