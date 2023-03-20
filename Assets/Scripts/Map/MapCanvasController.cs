using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(!MapManager.Instance) return;
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

    public void ShowCombatPanel(bool state)
    {
        // Combat.SetActive(state);
    }
    public void BackToMapButton()
    {
        MapManager.Instance.UnloadScenes();
    }

    public void ShowEndGamePanel()
    {
        EndGame.SetActive(true);
    }

    public void RestartGame()
    {
        MapManager.Instance.RestartGame();
    }
}
