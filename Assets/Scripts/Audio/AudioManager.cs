using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public bool isPlay;
    public static AudioManager audioManager;




    public FMODUnity.EventReference musicTest;
    /*public FMODUnity.EventReference MenuClick;
    public FMODUnity.EventReference SelectSlot;
    public FMODUnity.EventReference PlaceItem;
    public FMODUnity.EventReference EnterShop;*/





    private void Awake()
    {
        if(audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
            PlayBackGroundSong();
        }


        
            
        
        DontDestroyOnLoad(audioManager);
    }
    

    public void PlaySoundEffect(string Effect)
    {
        RuntimeManager.PlayOneShot("event:/"+Effect);
    }

    public void PlayBackGroundSong()
    {
        
         RuntimeManager.PlayOneShot(musicTest);
        
    }

}
