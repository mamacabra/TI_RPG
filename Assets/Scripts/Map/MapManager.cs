using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance => instance ? instance : FindObjectOfType<MapManager>();
    public bool canClick = true;

    [SerializeField] private GameObject currentEventSystem;
    [SerializeField] private GameObject globalVolume;
    [SerializeField] private GameObject directionalLight;
    
    public event Action<bool> ShowPanel;
    public event Action<bool> ShowCombatPanel;
    public event Action ShowEndGamePanel;

    public event Action<bool> ShowMap;
    public event Action<Vector3, Vector3> MoveCameraDeslocate;
    public event Action ZoomCameraIsland;
    public event Action ResetCameras;

    public int ShipIndex = 0;
    MapNodeTest lastMp;
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

        if (!Transition.instance) return;
        Transition.instance.EndTransitionLoad -= EndTransitionLoad;
        Transition.instance.EndTransitionUnload -= EndTransitionUnload;
    }

    private void Start()
    {
        ShowMap?.Invoke(true);
        canClick = false;

        StartCoroutine(WaitToEndAnim());
        IEnumerator WaitToEndAnim()
        {
            yield return new WaitForSeconds(8f);
            ShowMap?.Invoke(false);
            ShowInventary();
            //canClick = true;
        }
    }
    
    void ShowInventary()
    {
        canClick = false;
        SceneNames n = SceneNames.SampleInventory;
        Action<bool> action = ShowPanel;
        StartTransition(n, action);
        currentEventSystem.SetActive(false); globalVolume.SetActive(false); directionalLight.SetActive(false);
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
                /*if (mp.parent[0] == lastMp)
                {*/
                    lastMp = mp;
                    lastMpHighlight = mp;
                    //TutorialManager.instance.DoNextTutorial();
                    return true;
                //}
            }

            foreach (var p in mp.parent)
            {
                if (p == lastMp)
                    countChildrensAndParents++;
            }

            if (countChildrensAndParents >= 1)
            {
                lastMp = mp;
                lastMpHighlight = mp;
               // TutorialManager.instance.DoNextTutorial();
                return true;
            }
        }

        if (mp.Depth == 0)
        {
            lastMp = mp;
            lastMpHighlight = mp;
            ShipIndex--;
           // TutorialManager.instance.DoNextTutorial();
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
            if (i == 1) /*if (mp.parent[0] == lastMpHighlight)*/return true;

            foreach (var p in mp.parent) if (p == lastMpHighlight) countChildrensAndParents++;

            if (countChildrensAndParents >= 1)return true;
        }
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
            action = ShowPanel;

        }
        else if (lastMp.typeOfIsland == TypeOfIsland.CommonCombat)
        {

            ZoomCamera();

            n = lastMp.GetScene;
            action = ShowCombatPanel;
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

        StartTransition(n, action);
    }

    void StartTransition(SceneNames n, Action<bool> action)
    {
        currentEventSystem.SetActive(false); globalVolume.SetActive(false); directionalLight.SetActive(false);
        Transition.instance.TransitionScenes(n, LoadSceneMode.Additive, true, true);

        StartCoroutine(WaitToShowPanel());
        IEnumerator WaitToShowPanel()
        {
            yield return new WaitForSeconds(1f);
            currentEventSystem.SetActive(false); globalVolume.SetActive(false); directionalLight.SetActive(false);
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
       // if (TutorialManager.instance.isTutorial) { TutorialManager.instance?.DoNextTutorial(); }
    }

    public void OnCanClick()
    {
        Time.timeScale = 1;
        ShowPanel?.Invoke(false);
        ShowCombatPanel?.Invoke(false);
        canClick = true;
    }

    public void UnloadScenes(bool isCombatScene)
    {
        Debug.Log("UnloadScenes");
        SceneNames s = isCombatScene ? lastMp.GetScene : SceneNames.SampleInventory; 
        Transition.instance.TransitionScenes(s, LoadSceneMode.Additive, false, true);
       SaveDeath.Instance.CheckGameOver();

        if (EndGame)
        {
            GameOver();
            return;
        }
        
        OnCanClick();

        if (isCombatScene)
        {
            ShowInventary();

            StartCoroutine(WaitToDisable());
            IEnumerator WaitToDisable()
            {
                yield return new WaitForSeconds(1f);
                currentEventSystem.SetActive(false); globalVolume.SetActive(false); directionalLight.SetActive(false);
            }
           
        }
    }

    public void GameOver()
    {
        EndGame = true;
        Time.timeScale = 0;
        ShowEndGamePanel?.Invoke();
    }

    public void EndTransitionUnload()
    {
        DoNextTutorial(); currentEventSystem.SetActive(true); globalVolume.SetActive(true); directionalLight.SetActive(true);
    }

    public void RestartGame()
    {
        Transition.instance.TransitionScenes(SceneNames.SampleMap, LoadSceneMode.Single, true, false);
        Time.timeScale = 1;
    }
}
