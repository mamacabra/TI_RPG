using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Inventory;
using Utilities;


[Serializable]
public struct Panel
{
    public string panelName;
    public GameObject panel;
    //public Button[] buttons;
}

public class MenuController : MonoBehaviour
{
    [Header("Paineis")] [SerializeField] List<Panel> panels = new List<Panel>();
    [SerializeField] private string firtsScreenToShow;

    private void Start()
    {
        JsonStorage.DeleteFile(Constants.SaveFile.Inventory);
        SetScreenActive(firtsScreenToShow);
    }

    public void PlayGame()
    {
        Transition.instance.TransitionScenes(SceneNames.Map, LoadSceneMode.Single, true, false);
    }
    public void SetScreenActive(string p)
    {
        Transition.instance.ShowTransition();

        StartCoroutine(WaitToHideTransition());
        IEnumerator WaitToHideTransition()
        {
            yield return new WaitForSeconds(1f);
            
            foreach (var s in panels)
                s.panel.SetActive(false);
            
            GameObject gPanel = panels.Find(c => c.panelName == p).panel;
            gPanel.SetActive(true);
            
            Transition.instance.HideTransition();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
