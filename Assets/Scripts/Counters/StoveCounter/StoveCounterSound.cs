// using System;
// using UnityEngine;

// public class StoveCounterSound : MonoBehaviour
// {
//     [SerializeField] Stove stove;
//     // private AudioSource audioSource;
//     [SerializeField] private AudioClipsSO audioClipsSO;


//     public static event EventHandler<OnItemFryingEventArgs> OnItemFrying; // event to trigger sound when a item is dropped
//     public class OnItemFryingEventArgs : EventArgs { 
//         public bool play;
//         public Transform stovePos;
//     }

//     // private void Awake(){
//     //     audioSource = GetComponent<AudioSource>();
//     //     Debug.Log("We Got the Audio Source");
//     // }

//     private void Start(){
//         stove.OnStateChanged += OnStateChanged;
//     }

//     void OnStateChanged(object sender, Stove.OnstateChangedEventArgs e){
//         bool playSound = e.state == Stove.State.Frying || e.state == Stove.State.Fried;
//         if(playSound){
            
//             // audioSource.Play();
//             // SoundManager.Instance.PlaySound(audioSource, Camera.main.transform.position, SoundManager.Instance.getVolume());
//             // PlaySound(audioClipsSO.sizzle, stove.transform.position, SoundManager.Instance.getVolume());
//             OnItemFrying?.Invoke(this, new OnItemFryingEventArgs{play = true, stovePos = stove.transform});
//         }else{
//             // audioSource.Pause();
//             // PlaySound(audioClipsSO.sizzle, stove.transform.position, 0f);
//             OnItemFrying?.Invoke(this, new OnItemFryingEventArgs{play = false, stovePos = stove.transform});
//         }
//     }

//     // Function to play sound
//     public void PlaySound(AudioClip audioClip, Vector3 pos, float volume){
//         AudioSource.PlayClipAtPoint(audioClip, pos, volume);
//     }
// }
