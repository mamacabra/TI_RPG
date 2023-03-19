using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject StoreOrCampPanel;
    
    private void OnEnable()
    {
        MapManager.Instance.ShowPanel += ShowPanel;
    }

    private void OnDisable()
    {
        if(!MapManager.Instance) return;
        MapManager.Instance.ShowPanel -= ShowPanel;
    }

    public void ShowPanel()
    {
        StoreOrCampPanel.SetActive(true);
    }

    public void BackButton()
    {
        MapManager.Instance.OnCanClick();
    }
}
