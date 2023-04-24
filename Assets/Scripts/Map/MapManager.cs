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

    public int ShipIndex = 0;
    [SerializeField] MapNodeTest lastMp;
    [SerializeField] private GameObject map;

    public bool EndGame;
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

    public void CheckIsland()
    {
        if (lastMp.typeOfIsland == TypeOfIsland.StoreOrForge || lastMp.typeOfIsland == TypeOfIsland.Camp)
        {
            Time.timeScale = 0;
            ShowPanel?.Invoke(true);
        }
        else if (lastMp.typeOfIsland == TypeOfIsland.CommonCombat)
        {
            //map.SetActive(false);
            ShowCombatPanel?.Invoke(true);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lastMp.GetScene, LoadSceneMode.Additive);
        }
        else if (lastMp.typeOfIsland == TypeOfIsland.BossCombat)
        {
            EndGame = true;
            ShowCombatPanel?.Invoke(true);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleCombat", LoadSceneMode.Additive);
        }
        else
        {
            OnCanClick();
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
