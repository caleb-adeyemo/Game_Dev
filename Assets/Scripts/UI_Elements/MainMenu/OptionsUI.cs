using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour{
    public static OptionsUI Instance { get; private set; }

    // Event triggered when slider value changes
    public delegate void SliderChangeEvent(float volume);
    public event SliderChangeEvent OnSliderChange;

    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        Instance = this;
        // Subscribe to the slider's OnValueChanged event
        musicSlider.onValueChanged.AddListener(OnMusicSliderChange);

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }

    private void OnMusicSliderChange(float volume)
    {
        // Trigger the OnSliderChange event with the new volume value
        OnSliderChange?.Invoke(volume);
    }
}
