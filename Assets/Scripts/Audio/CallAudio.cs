using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAudio : MonoBehaviour
{
    [SerializeField] string ClickEffect;
    [SerializeField] string HoverEffect;
    [SerializeField] string NewCardEffect;
    void Start()
    {
        if(NewCardEffect!= null)
            AudioManager.audioManager.PlaySoundEffect(NewCardEffect);
    }

    private void OnMouseDown()
    {
        if(ClickEffect != null)
            AudioManager.audioManager.PlaySoundEffect(ClickEffect);
    }
    private void OnMouseEnter()
    {
        if(HoverEffect != null)
            AudioManager.audioManager.PlaySoundEffect(HoverEffect);
    }
}
