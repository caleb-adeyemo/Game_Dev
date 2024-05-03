using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

private float volume = 1f;
[SerializeField] private AudioClipsSO audioClipsSO;
  private void Start(){
    DeleveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
    DeleveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
    CuttingCounter.OnAnyCut += OnAnyCut;
    Player.Instance.OnPlayerPickUp += OnPlayerPickUp;
    BaseCounter.OnItemDrop += OnItemDrop;
    Bin.OnItemTrashed += OnItemTrashed;
    // Subscribe to the OnSliderChange event of the OptionsUI
    OptionsUI.Instance.OnSliderSfxChange += SetVolume;
  }

  private void SetVolume(float _volume){
    // Debug.Log("Second Trigger: "+ _volume);
    volume = _volume;
  }

  // Success
  private void OnRecipeSuccess(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.deleverySuccess, Camera.main.transform.position, volume);
  }

  // Failed
  private void OnRecipeFailed(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.deleveryFail, Camera.main.transform.position, volume);
  }

  // Chop
  private void OnAnyCut(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.chop, Camera.main.transform.position, volume);
  }

  // Pick Up
  private void OnPlayerPickUp(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.pickUp, Camera.main.transform.position, volume);
  }

  // Drop Item
  private void OnItemDrop(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.drop, Camera.main.transform.position, volume);
  }

  // Trahsed Item
  private void OnItemTrashed(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.trash, Camera.main.transform.position, volume);
  }
  

  // Function to play sound
  private void PlaySound(AudioClip audioClip, Vector3 pos, float volume){
    AudioSource.PlayClipAtPoint(audioClip, pos, volume);
  }
}
