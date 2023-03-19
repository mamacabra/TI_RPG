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
    public event Action ShowPanel;

    public int ShipIndex = 0;
    [SerializeField] MapNodeTest lastMp;
    [SerializeField] private GameObject map;

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
                Debug.Log("oi");
                lastMp = island.GetComponent<MapNodeTest>();
                return true;
            }
        }

        ShipIndex--;
        return false;
    }

    public void CheckIsland()
    {
        if (lastMp.typeOfIsland == TypeOfIsland.StoreOrForge || lastMp.typeOfIsland == TypeOfIsland.Camp)
        {
            Time.timeScale = 0;
            ShowPanel?.Invoke();
        }
        else
        {
            map.SetActive(false);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleCombat", LoadSceneMode.Additive);
        }
    }

    public void OnCanClick()
    {
        CanClick?.Invoke();
        Time.timeScale = 1;
    }

    public void UnloadScenes()
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync ("SampleCombat");
        map.SetActive(true);
    }
}
