using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
   public void CallSfx(string Effect)
   {
        AudioManager.audioManager.PlaySoundEffect(Effect);
   }
}
