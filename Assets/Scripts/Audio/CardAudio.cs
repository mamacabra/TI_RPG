using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAudio : MonoBehaviour
{
    [SerializeField] string ClickEffect;
    [SerializeField] string HoverEffect;
    [SerializeField] string NewCardEffect;
    void Start()
    {
        AudioManager.audioManager.PlaySoundEffect(NewCardEffect + Random.Range(1, 5).ToString());
    }

    private void OnMouseDown()
    {
        AudioManager.audioManager.PlaySoundEffect(ClickEffect);
    }
    private void OnMouseEnter()
    {
        AudioManager.audioManager.PlaySoundEffect(HoverEffect + Random.Range(1,5).ToString());
    }
}
