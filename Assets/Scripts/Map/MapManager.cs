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
    MapNodeTest lastMpHighlight;
    [SerializeField] private GameObject map;

    [HideInInspector] public bool EndGame;
     public bool shipArrived = false;
    [HideInInspector] public bool zoomCamOver = false;

    private void OnEnable()
    {
        Transition.instance.EndTransitionLoad += EndTransitionLoad;
        Transition.instance.EndTransitionUnload += EndTransitionUnload;
    }

    private void OnDisable()
    {
        
        if(!Transition.instance) return;
        Transition.instance.EndTransitionLoad -= EndTransitionLoad;
        Transition.instance.EndTransitionUnload -= EndTransitionUnload;
    }

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
    public bool CheckIndexHighlight(GameObject island)
    {
        int i = ShipIndex;
        i++;
        int countChildrensAndParents = 0;
        MapNodeTest mp = island.GetComponent<MapNodeTest>();
        if (mp.Depth == i)
        {
            if (i == 1)
            {
                if (mp.parent[0] == lastMpHighlight)return true;
            }

            foreach (var p in mp.parent)
            {
                if (p == lastMpHighlight)countChildrensAndParents++;
            }

            if (countChildrensAndParents >= 1)return true;
        }

        if (mp.Depth == 0)return true;
        
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
        //camera
        SceneNames n = SceneNames.SampleCombat;
        Action<bool> action = null;
        if (lastMp.typeOfIsland == TypeOfIsland.StoreOrForge || lastMp.typeOfIsland == TypeOfIsland.Camp)
        {
           
            ZoomCamera();
          
           n = SceneNames.SampleInventory;
           action =  ShowPanel;

        }
        else if (lastMp.typeOfIsland == TypeOfIsland.CommonCombat)
        {
           
            ZoomCamera();
           
            n = lastMp.GetScene;
            action =  ShowCombatPanel;
        }
        else if (lastMp.typeOfIsland == TypeOfIsland.BossCombat)
        {
            ZoomCamera();
           
            n = SceneNames.SampleCombat;
            EndGame = true;
            action = ShowCombatPanel;
        }
        else
        {
            OnCanClick();
        }

        currentEventSystem.SetActive(false); globalVolume.SetActive(false); directionalLight.SetActive(false);
        Transition.instance.TransitionScenes(n,LoadSceneMode.Additive, true, true);

        StartCoroutine(WaitToShowPanel());
        IEnumerator WaitToShowPanel()
        {
            yield return new WaitForSeconds(1f);
            action?.Invoke(true);
        }
    }

    public void EndTransitionLoad()
    {
        StartCoroutine(WaitToNextTutorial());
        IEnumerator WaitToNextTutorial()
        {
            DoNextTutorial();
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
        Debug.Log("UnloadScenes");
        SceneNames s = isCombatScene ?lastMp.GetScene : SceneNames.SampleInventory;
        Transition.instance.TransitionScenes(s,LoadSceneMode.Additive, false, true);
       
        
        if (EndGame)
        {
            Time.timeScale = 0;
            ShowEndGamePanel?.Invoke();
            return;
        }

        OnCanClick();
    }

    public void EndTransitionUnload()
    {
        DoNextTutorial(); currentEventSystem.SetActive(true); globalVolume.SetActive(true); directionalLight.SetActive(true);
    }

    public void RestartGame()
    {
        Transition.instance.TransitionScenes(SceneNames.SampleMap,LoadSceneMode.Single, true, false);
        Time.timeScale = 1;
    }
}
