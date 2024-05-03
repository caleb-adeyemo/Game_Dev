using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

  private void Start(){
    DeleveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
    DeleveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
  }

  private void OnRecipeSuccess(object sender, System.EventArgs e){

  }

  private void OnRecipeFailed(object sender, System.EventArgs e){
    
  }
  private void PlaySound(AudioClip audioClip, Vector3 pos, float volume = 1f){
    AudioSource.PlayClipAtPoint(audioClip, pos, volume);
  }
}
