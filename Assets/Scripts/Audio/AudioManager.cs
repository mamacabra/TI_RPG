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
    private void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
            instance = FMODUnity.RuntimeManager.CreateInstance(musicTest);
            instance.start();
            SetSong((int)SongName.Map);
        }
    }

    private void Update()
    {
        
       /* if(aux != 0 || aux != 1)
        {
            if (songType == 1 && aux > 0)
            {
                aux -= Time.deltaTime / 2;
                if (aux < 0)
                    aux = -0.9f;
            }

            if (songType == 0 && aux < 1)
            {
                aux += Time.deltaTime / 2;
                if (aux > 1)
                    aux = 1.1f;
            }

            Debug.Log(aux);
            instance.setParameterByName("MapaOuCombate", aux);
        }    */
        
        

    }


    public void PlaySoundEffect(string Effect)
    {
        RuntimeManager.PlayOneShot("event:/"+Effect);
    }

    public void SetSong(int s)
    {
        StartCoroutine(Enume());

        IEnumerator Enume()
        {
            float i = 0;
            


            float rate = 0.5f;

            if((int)s == 0)
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
