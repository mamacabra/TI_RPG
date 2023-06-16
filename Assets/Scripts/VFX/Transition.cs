using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneNames
{
    SampleMap,
    Menu,
    SampleCombat,
    SampleCombat_cavern_1,
    SampleCombat_cavern_2,
    SampleInventory,
    SampleInventoryLoadResource,
    SampleSaveInventory,
    SampleVFXCombat
    
}
public class Transition : MonoBehaviour
{
    public static Transition instance;
    [SerializeField] private Image transition;

    private float showTransitionTime = 1f;
    private float hideTransitionTime = 1f;

    public event Action EndTransitionLoad;
    public event Action EndTransitionUnload;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transition.transform);
    }

    private void Start()
    {
        transition.DOFade(0, 0).OnComplete(()=>{transition.gameObject.SetActive(false);});
    }

    public void ShowTransition()
    {
        transition.gameObject.SetActive(true);
        transition.DOFade(1, showTransitionTime);
    }
    
    public void HideTransition()
    {
        transition.DOFade(0, hideTransitionTime).OnComplete(()=>{transition.gameObject.SetActive(false);});
    }

    public void TransitionScenes(SceneNames sceneName,LoadSceneMode mode,bool load)
    {
        ShowTransition();
        StartCoroutine(Transition());
        IEnumerator Transition()
        {
            yield return new WaitForSeconds(showTransitionTime);
            AsyncOperation asyncLoad = load? SceneManager.LoadSceneAsync (sceneName.ToString(), mode) : SceneManager.UnloadSceneAsync(sceneName.ToString());

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            if(load)
                EndTransitionLoad?.Invoke();
            else
                EndTransitionUnload?.Invoke();
            
            yield return new WaitForSeconds(hideTransitionTime);
            HideTransition();
        }
    }
}
