using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject StoreOrCampPanel;
    [SerializeField] private GameObject Combat;
    [SerializeField] private GameObject EndGame;

    private void OnEnable()
    {
        MapManager.Instance.ShowPanel += ShowPanel;
        MapManager.Instance.ShowCombatPanel += ShowCombatPanel;
        MapManager.Instance.ShowEndGamePanel += ShowEndGamePanel;

    }

    private void OnDisable()
    {
        if (!MapManager.Instance) return;
        MapManager.Instance.ShowPanel -= ShowPanel;
        MapManager.Instance.ShowCombatPanel -= ShowCombatPanel;
        MapManager.Instance.ShowEndGamePanel -= ShowEndGamePanel;
    }

    public void ShowPanel(bool state)
    {
        StoreOrCampPanel.SetActive(state);
    }
    public void BackButton()
    {
        MapManager.Instance.OnCanClick();
    }

    public void BackToMenu()
    {
        Debug.Log("BackToMenu");
        Transition.instance.TransitionScenes(SceneNames.Menu, LoadSceneMode.Single, true, false);
    }
    public void ShowCombatPanel(bool state)
    {
        // Combat.SetActive(state);
    }
    public void BackToMapButton(bool isCombatScene)
    {
        if (InventoryUIManager.instance)
        {
            if (InventoryUIManager.instance.VerifyCards())
            {
                MapManager.Instance.UnloadScenes(isCombatScene);
            }
        }
        else
        {
            MapManager.Instance.UnloadScenes(isCombatScene);
        }
    }

    public void ShowEndGamePanel()
    {
        EndGame.SetActive(true);
        Transition.instance.HideTransition();
    }

    public void RestartGame()
    {
        MapManager.Instance.RestartGame();
    }
}
