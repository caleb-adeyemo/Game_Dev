using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] Stove stove;
    private AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Start(){
        stove.OnStateChanged += OnStateChanged;
    }

    void OnStateChanged(object sender, Stove.OnstateChangedEventArgs e){
        bool playSound = e.state == Stove.State.Frying || e.state == Stove.State.Fried;
        if(playSound){
            audioSource.Play();
        }else{
            audioSource.Stop();
        }
    }
}
