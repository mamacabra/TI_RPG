using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public bool isPlay;
    public static AudioManager audioManager;

    public FMODUnity.EventReference musicTest;

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
    
    public void PlayBackGroundSong()
    {
        
         RuntimeManager.PlayOneShot(musicTest);
        
    }

}
