using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DG.Tweening;

public enum SongName { Map = 0, Combat = 1}



public class AudioManager : MonoBehaviour
{
    public bool isPlay;
    public static AudioManager audioManager;

    private FMOD.Studio.EventInstance instance;


    public FMODUnity.EventReference musicTest;
    /*public FMODUnity.EventReference MenuClick;
    public FMODUnity.EventReference SelectSlot;
    public FMODUnity.EventReference PlaceItem;
    public FMODUnity.EventReference EnterShop;*/

    public float songType;

    public bool taMudando;

    float aux = 0;
    private int currentSong = 0;
    private void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            currentSong = 0;
            audioManager = this;
            instance = FMODUnity.RuntimeManager.CreateInstance(musicTest);
            instance.start();
        }
        DontDestroyOnLoad(this);
    }
    
    public void PlaySoundEffect(string Effect)
    {
        RuntimeManager.PlayOneShot("event:/"+Effect);
    }

    public void SetSong(int s)
    {
        if(s == currentSong) return;
        currentSong = s;
        StartCoroutine(Enume());

        IEnumerator Enume()
        {
            float i = 0;

            float rate = 0.5f;

            if(s == 0)
            {
                i = 1;
                while (i > 0)
                {
                    i -= Time.deltaTime * rate;
                    instance.setParameterByName("MapaOuCombate", i);
                    yield return null;
                }
            }
            else
            {
                while (i < 1)
                {
                    i += Time.deltaTime * rate;
                    instance.setParameterByName("MapaOuCombate", i);
                    yield return null;
                }
                
            }
                
        }

    }



}
