using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] Stove stove;
    // private AudioSource audioSource;
    [SerializeField] private AudioClipsSO audioClipsSO;

    // private void Awake(){
    //     audioSource = GetComponent<AudioSource>();
    //     Debug.Log("We Got the Audio Source");
    // }

    private void Start(){
        stove.OnStateChanged += OnStateChanged;
    }

    void OnStateChanged(object sender, Stove.OnstateChangedEventArgs e){
        bool playSound = e.state == Stove.State.Frying || e.state == Stove.State.Fried;
        if(playSound){
            
            // audioSource.Play();
            // SoundManager.Instance.PlaySound(audioSource, Camera.main.transform.position, SoundManager.Instance.getVolume());
            PlaySound(audioClipsSO.sizzle, stove.transform.position, SoundManager.Instance.getVolume());
        }else{
            // audioSource.Pause();
            PlaySound(audioClipsSO.sizzle, stove.transform.position, 0f);
        }
    }

    // Function to play sound
    public void PlaySound(AudioClip audioClip, Vector3 pos, float volume){
        AudioSource.PlayClipAtPoint(audioClip, pos, volume);
    }
}
