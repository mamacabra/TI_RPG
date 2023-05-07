using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance => instance ? instance : FindObjectOfType<MapManager>();

    public event Action CanClick;
    public event Action<bool> ShowPanel;
    public event Action<bool> ShowCombatPanel;
    public event Action ShowEndGamePanel;

    public event Action<Vector3,Vector3> MoveCameraDeslocate;
    public event Action ZoomCameraIsland;
    public event Action ResetCameras;

    public int ShipIndex = 0;
    [SerializeField] MapNodeTest lastMp;
    [SerializeField] private GameObject map;

    [HideInInspector] public bool EndGame;
    [HideInInspector] public bool shipArrived = false;
    [HideInInspector] public bool zoomCamOver = false;

    public bool CheckIndex(GameObject island)
    {
        ShipIndex++;
        int countChildrensAndParents = 0;
        MapNodeTest mp = island.GetComponent<MapNodeTest>();
        if (mp.Depth == ShipIndex)
        {
            if (ShipIndex == 1)
            {
                if (mp.parent[0] == lastMp)
                {
                    lastMp = island.GetComponent<MapNodeTest>();
                    return true;
                }
            }

            foreach (var p in mp.parent)
            {
                if (p == lastMp)
                    countChildrensAndParents++;
            }

            if (countChildrensAndParents >= 1)
            {
                lastMp = island.GetComponent<MapNodeTest>();
                return true;
            }
        }

        if (mp.Depth == 0)
        {
            lastMp = island.GetComponent<MapNodeTest>();
            ShipIndex--;
            return true;
        }

        ShipIndex--;
        return false;
    }

    public void MoveCamera(Vector3 pos, Vector3 islandPos)
    {
        MoveCameraDeslocate?.Invoke(pos,islandPos);
    }

    public void ZoomCamera()
    {
        ZoomCameraIsland?.Invoke();
    }

    public void CheckIsland()
    {
        //Fade e camera
        if (lastMp.typeOfIsland == TypeOfIsland.StoreOrForge || lastMp.typeOfIsland == TypeOfIsland.Camp)
        {
            Time.timeScale = 0;
            ShowPanel?.Invoke(true);
        }
        else if (lastMp.typeOfIsland == TypeOfIsland.CommonCombat)
        {
            //map.SetActive(false);
            ZoomCamera();
            StartCoroutine(WaitToCheckIsland(lastMp.GetScene));
            ShowCombatPanel?.Invoke(true);
        }
        else if (lastMp.typeOfIsland == TypeOfIsland.BossCombat)
        {
            ZoomCamera();
            StartCoroutine(WaitToCheckIsland("SampleCombat"));
            EndGame = true;
            ShowCombatPanel?.Invoke(true);
        }
        else
        {
            OnCanClick();
        }

        IEnumerator WaitToCheckIsland(string scene)
        {
            yield return new WaitUntil(() => zoomCamOver);
            yield return new WaitForSeconds(0.1f);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            zoomCamOver = false;
            yield return new WaitForSeconds(1f);
            ResetCameras?.Invoke();
        }
    }

    public void OnCanClick()
    {
        Time.timeScale = 1;
        ShowPanel?.Invoke(false);
        ShowCombatPanel?.Invoke(false);
        CanClick?.Invoke();
    }

    public void UnloadScenes()
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(lastMp.GetScene);
        if (EndGame)
        {
            Time.timeScale = 0;
            ShowEndGamePanel?.Invoke();
            return;
        }

        OnCanClick();
        //map.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleMap");
        Time.timeScale = 1;
    }
}
