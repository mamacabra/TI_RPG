using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance => instance ? instance : FindObjectOfType<MapManager>();

    [SerializeField] private GameObject currentEventSystem;
    [SerializeField] private GameObject globalVolume;
    [SerializeField] private GameObject directionalLight;

    public event Action CanClick;
    public event Action<bool> ShowPanel;
    public event Action<bool> ShowCombatPanel;
    public event Action ShowEndGamePanel;

    public event Action<Vector3, Vector3> MoveCameraDeslocate;
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
        MoveCameraDeslocate?.Invoke(pos, islandPos);
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
            // Time.timeScale = 0;
            ZoomCamera();
            StartCoroutine(WaitToCheckIsland("SampleInventory"));
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
            currentEventSystem.SetActive(false); globalVolume.SetActive(false); directionalLight.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            asyncLoad.completed += delegate { DoNextTutorial(); };
            zoomCamOver = false;
            yield return new WaitForSeconds(1f);
            ResetCameras?.Invoke();
        }
    }
    private void DoNextTutorial()
    {
        if (TutorialManager.instance.isTutorial) { TutorialManager.instance?.DoNextTutorial(); }
    }

    public void OnCanClick()
    {
        Time.timeScale = 1;
        ShowPanel?.Invoke(false);
        ShowCombatPanel?.Invoke(false);
        CanClick?.Invoke();
    }

    public void UnloadScenes(bool isCombatScene)
    {
        AsyncOperation asyncUnload = isCombatScene ? SceneManager.UnloadSceneAsync(lastMp.GetScene) : SceneManager.UnloadSceneAsync("SampleInventory");
        asyncUnload.completed += delegate { DoNextTutorial(); currentEventSystem.SetActive(true); globalVolume.SetActive(true); directionalLight.SetActive(true); };
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
