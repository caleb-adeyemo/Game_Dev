using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour{
  [SerializeField] private AudioClipsSO audioClipsSO;
  [SerializeField] private AudioMixerGroup success_mixer; // Reference to the Success SFX Audio Mixer group
  [SerializeField] private AudioMixerGroup fail_mixer; // Reference to the fail SFX Audio Mixer group
  [SerializeField] private AudioMixerGroup chop_mixer; // Reference to the Chop SFX Audio Mixer group
  [SerializeField] private AudioMixerGroup trash_mixer; // Reference to the Trash SFX Audio Mixer group
  [SerializeField] private AudioMixerGroup pick_up_mixer; // Reference to the Pick Up SFX Audio Mixer group
  [SerializeField] private AudioMixerGroup drop_mixer; // Reference to the Drop SFX Audio Mixer group
  [SerializeField] private AudioMixerGroup fry_mixer; // Reference to the Fry SFX Audio Mixer group


private AudioSource fryingAudioSource; // Reference to the AudioSource for sizzling audio
private bool isFryingAudioPlaying = false; // Flag to track if sizzling audio is currently playing


  private void Start(){
    // Subscribe to the event when an order is sucessfully delivered
    DeleveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
    // Subscribe to the event when a Failed order is delivered
    DeleveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
    // Subscribe to the event when a recipe is being cut
    CuttingCounter.OnAnyCut += OnAnyCut;
    // Subscribe to the event when the player picks up an item
    Player.Instance.OnPlayerPickUp += OnPlayerPickUp;
    // Subscribe to the event when the player drops an item on the counter
    BaseCounter.OnItemDrop += OnItemDrop;
    // Subscribe to the event when the player drops an item on a plate
    Plate.OnItemDrop += OnItemDrop;
    // Subscribe to the event when the player bins an item
    Bin.OnItemTrashed += OnItemTrashed;
    // Subscribe to the event when the player is frying an item
    Stove.OnStateChanged += OnStateChanged;
  }

  // Successful Order
  private void OnRecipeSuccess(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.deleverySuccess, success_mixer);
  }

  // Failed Order
  private void OnRecipeFailed(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.deleveryFail,fail_mixer);
  }

  // Chop Ingredient
  private void OnAnyCut(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.chop, chop_mixer);
  }

  // Pick Up Item
  private void OnPlayerPickUp(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.pickUp, pick_up_mixer);
  }

  // Drop Item
  private void OnItemDrop(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.drop, drop_mixer);
  }

  // Trahsed Item
  private void OnItemTrashed(object sender, System.EventArgs e){
    PlaySound(audioClipsSO.trash, trash_mixer);
  }

  // Frying Item
  private void OnStateChanged(object sender, Stove.OnstateChangedEventArgs e){
      // Check if the state is transitioning to "Frying" or "Fried"
      if ((e.state == Stove.State.Frying || e.state == Stove.State.Fried) && !isFryingAudioPlaying) {
          // Play sizzling audio if it's not already playing
          fryingAudioSource = PlaySound(audioClipsSO.sizzle, fry_mixer);
          isFryingAudioPlaying = true; // Set flag to indicate that sizzling audio is now playing
      } else if (e.state != Stove.State.Frying && e.state != Stove.State.Fried && isFryingAudioPlaying) {
          // Stop sizzling audio if state changes from "Frying" or "Fried" to any other state
          StopSound(fryingAudioSource);
          isFryingAudioPlaying = false; // Reset flag
      }
  }


  // Function to play sound with specified mixer group
    public AudioSource PlaySound(AudioClip audioClip, AudioMixerGroup mixerGroup) {
        GameObject soundGameObject = new GameObject("OneShotAudio"); // Create a new GameObject to play the audio clip
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        audioSource.clip = audioClip; // Set the audio clip to play
        audioSource.outputAudioMixerGroup = mixerGroup; // Assign the specified Audio Mixer group
        audioSource.Play(); // Play the audio clip
        Destroy(soundGameObject, audioClip.length); // Destroy the GameObject after the audio clip has finished playing
        return audioSource;
    }


// Function to stop sound
private void StopSound(AudioSource audioSource) {
    if (audioSource != null && audioSource.isPlaying) {
        audioSource.Stop();
        Destroy(audioSource.gameObject); // Destroy the AudioSource GameObject
    }
}

}
