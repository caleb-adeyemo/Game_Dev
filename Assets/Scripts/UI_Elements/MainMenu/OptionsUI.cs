using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour{
    public static OptionsUI Instance { get; private set; }

    // Event triggered when slider value changes
    public delegate void SliderChangeEvent(float volume);

    public event SliderChangeEvent OnSliderChange;
    public event SliderChangeEvent OnSliderSfxChange;


    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        Instance = this;
        // Subscribe to the slider's OnValueChanged event
        musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderChange);
        // Deactivate the GameObject
        gameObject.SetActive(false);
    }

    private void OnMusicSliderChange(float volume)
    {
        // Trigger the OnSliderChange event with the new volume value
        OnSliderChange?.Invoke(volume);
    }

    private void OnSfxSliderChange(float volume)
    {
        // Trigger the OnSliderChange event with the new volume value
        OnSliderSfxChange?.Invoke(volume);
        // Debug.Log("First trigger" + volume);
    }
}
