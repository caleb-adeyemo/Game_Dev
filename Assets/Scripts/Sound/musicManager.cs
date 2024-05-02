using UnityEngine;

public class MusicManager : MonoBehaviour{
    private AudioSource audioSource;

    private void Start(){
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Subscribe to the OnSliderChange event of the OptionsUI
        OptionsUI.Instance.OnSliderChange += SetVolume;
    }

    // Method to set the volume of the AudioSource
    public void SetVolume(float volume){
        if (audioSource != null){
            Debug.Log("Volume: " + volume);
            // Clamp the volume between 0 and 1
            volume = Mathf.Clamp01(volume);

            // Set the volume of the AudioSource
            audioSource.volume = volume;
        }
    }
}
