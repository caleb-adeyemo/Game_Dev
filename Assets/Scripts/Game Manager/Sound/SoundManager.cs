using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClipsSO audioClipsSO;
    [SerializeField] private AudioMixerGroup success_mixer;
    [SerializeField] private AudioMixerGroup fail_mixer;
    [SerializeField] private AudioMixerGroup chop_mixer;
    [SerializeField] private AudioMixerGroup trash_mixer;
    [SerializeField] private AudioMixerGroup pick_up_mixer;
    [SerializeField] private AudioMixerGroup drop_mixer;
    [SerializeField] private AudioMixerGroup fry_mixer;
    [SerializeField] private AudioMixerGroup walk_mixer;

    private AudioSource fryingAudioSource;
    private bool isFryingAudioPlaying = false;

    private AudioSource walkingAudioSource;
    private bool isWalkingAudioPlaying = false;

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SubscribeToEvents();
    }

    private void OnSceneUnloaded(Scene scene) {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents() {
        DeleveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
        DeleveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
        CuttingCounter.OnAnyCut += OnAnyCut;
        Player.Instance.OnPlayerPickUp += OnPlayerPickUp;
        BaseCounter.OnItemDrop += OnItemDrop;
        Plate.OnItemDrop += OnItemDrop;
        Bin.OnItemTrashed += OnItemTrashed;
        Stove.OnStateChanged += OnStateChanged;
        Player.OnWalking += OnWalking;
    }

    private void UnsubscribeFromEvents() {
        DeleveryManager.Instance.OnRecipeSuccess -= OnRecipeSuccess;
        DeleveryManager.Instance.OnRecipeFailed -= OnRecipeFailed;
        CuttingCounter.OnAnyCut -= OnAnyCut;
        Player.Instance.OnPlayerPickUp -= OnPlayerPickUp;
        BaseCounter.OnItemDrop -= OnItemDrop;
        Plate.OnItemDrop -= OnItemDrop;
        Bin.OnItemTrashed -= OnItemTrashed;
        Stove.OnStateChanged -= OnStateChanged;
        Player.OnWalking -= OnWalking;
    }

    private void OnRecipeSuccess(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.deleverySuccess, success_mixer);
    }

    private void OnRecipeFailed(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.deleveryFail, fail_mixer);
    }

    private void OnAnyCut(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.chop, chop_mixer);
    }

    private void OnPlayerPickUp(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.pickUp, pick_up_mixer);
    }

    private void OnItemDrop(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.drop, drop_mixer);
    }

    private void OnItemTrashed(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.trash, trash_mixer);
    }

    private void OnStateChanged(object sender, Stove.OnstateChangedEventArgs e) {
        if ((e.state == Stove.State.Frying || e.state == Stove.State.Fried) && !isFryingAudioPlaying) {
            fryingAudioSource = PlaySound(audioClipsSO.sizzle, fry_mixer);
            isFryingAudioPlaying = true;
        } else if (e.state != Stove.State.Frying && e.state != Stove.State.Fried && isFryingAudioPlaying) {
            StopSound(fryingAudioSource);
            isFryingAudioPlaying = false;
        }
    }

    private void OnWalking(object sender, Player.Walk e) {
        if (e.isWalking && !isWalkingAudioPlaying) {
            walkingAudioSource = PlaySound(audioClipsSO.walking, walk_mixer);
            isWalkingAudioPlaying = true;
        } else if (!e.isWalking && isWalkingAudioPlaying) {
            StopSound(walkingAudioSource);
            isWalkingAudioPlaying = false;
        }
    }

    private AudioSource PlaySound(AudioClip audioClip, AudioMixerGroup mixerGroup) {
        GameObject soundGameObject = new GameObject("OneShotAudio");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.Play();
        Destroy(soundGameObject, audioClip.length);
        return audioSource;
    }

    private void StopSound(AudioSource audioSource) {
        if (audioSource != null && audioSource.isPlaying) {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
        }
    }
}
