using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


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
        SetScreenActive(firtsScreenToShow);
    }

    public void PlayGame()
    {
        Transition.instance.TransitionScenes(SceneNames.SampleMap1, LoadSceneMode.Single, true, false);
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
