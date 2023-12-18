using System.Collections;
using System.Collections.Generic;
using Combat;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject StoreOrCampPanel;
    [SerializeField] private GameObject Combat;
    [SerializeField] private GameObject EndGame;
    [SerializeField] private GameObject menuPopup;

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

    public void ShowMenu(bool show)
    {
        if (show)
        {
            Time.timeScale = 0;
            menuPopup.transform.localScale = Vector3.zero;
            menuPopup.SetActive(true);
            menuPopup.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        }
        else
        {
            Time.timeScale = 1;
            menuPopup.transform.DOScale(0, 0.25f).SetUpdate(true).OnComplete(() =>
            {
                menuPopup.transform.localScale = Vector3.zero;
                menuPopup.SetActive(false);
            });
        }
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
        AudioManager.audioManager.SetSong((int)SongName.Map);
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
        if (CombatState.Instance)
        {
            if(CombatState.Instance.GetState() == CombatStateType.Defeat){
                if(EndGame.transform.GetChild(3).TryGetComponent(out TextMeshProUGUI endGameText))
                    endGameText.text = "Você falhou ao tentar restaurar o equilíbrio do mar.";
            }
            else{
                if(EndGame.transform.GetChild(3).TryGetComponent(out TextMeshProUGUI endGameText))
                    endGameText.text = "Você restaurou o equilíbrio do mar.";
            }
        }
        EndGame.SetActive(true);
        Transition.instance.HideTransition();
    }

    public void RestartGame()
    {
        MapManager.Instance.RestartGame();
    }
}
