using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  private void PlaySound(AudioClip audioClip, Vector3 pos, float volume = 1f){
    AudioSource.PlayClipAtPoint(audioClip, pos, volume);
  }
}
