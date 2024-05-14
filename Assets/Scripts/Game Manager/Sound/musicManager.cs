using UnityEngine;

public class MusicManager : MonoBehaviour{
    [SerializeField] private AudioSource audioSource;

    private void Start(){
        // Get the AudioSource component attached to the GameObject that the script is attached to
        // audioSource = GetComponent<AudioSource>();

        // Subscribe to the OnSliderChange event of the OptionsUI; this is the main music for each level; not the sfx!!!
        OptionsUI.Instance.OnSliderChange += SetVolume;
    }

    // Method to set the volume of the AudioSource
    public void SetVolume(float volume){
        if (audioSource != null){
            // Clamp the volume between 0 and 1
            volume = Mathf.Clamp01(volume);

            // Set the volume of the AudioSource
            audioSource.volume = volume;
        }
    }
}
