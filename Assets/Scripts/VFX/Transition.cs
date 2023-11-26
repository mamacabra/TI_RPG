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
    SampleMap1,
    Map,
    Menu,
    SampleCombat,
    SampleCombat_cavern_1,
    SampleCombat_cavern_2,
    SampleCombat_forest,
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
        //transition.DOFade(0, 0).OnComplete(()=>{transition.gameObject.SetActive(false);});
    }

    public void ShowTransition()
    {
        transition.gameObject.SetActive(true);
        transition.DOFade(1, showTransitionTime).SetUpdate(true);
    }
    
    public void HideTransition()
    {
        transition.DOFade(0, hideTransitionTime).SetUpdate(true).OnComplete(()=>{transition.gameObject.SetActive(false);});
    }

    public void TransitionScenes(SceneNames sceneName,LoadSceneMode mode,bool load, bool callAction)
    {

        if (sceneName == SceneNames.Map || sceneName == SceneNames.Menu ||sceneName == SceneNames.SampleInventory )
        {
            AudioManager.audioManager.SetSong((int)SongName.Map);
        }
        else
        {
            AudioManager.audioManager.SetSong((int)SongName.Combat);
        }
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

            if (callAction)
            {
                if (load)
                    EndTransitionLoad?.Invoke();
                else
                    EndTransitionUnload?.Invoke();
            }

            yield return new WaitForSeconds(hideTransitionTime);
            HideTransition();
        }
    }
}
