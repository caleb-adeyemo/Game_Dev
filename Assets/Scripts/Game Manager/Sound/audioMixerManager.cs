using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioMixerManager : MonoBehaviour{

    // Serialized Variables
    [SerializeField] private AudioMixer m_Mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider walkingSlider;
    [SerializeField] private Slider frying_Slider;

    // Variables
    private float musicLevel;
    private float sfxLevel;
    private float walkLevel;
    private float fryLevel;

    private void Start(){
        // Set the sliders to the right levl on start
        m_Mixer.GetFloat("MusicVolume", out musicLevel);
        musicSlider.value = musicLevel;

        m_Mixer.GetFloat("SfxVolume", out sfxLevel);
        sfxSlider.value = sfxLevel;

        m_Mixer.GetFloat("Walk", out walkLevel);
        walkingSlider.value = walkLevel;

        m_Mixer.GetFloat("Fry", out fryLevel);
        frying_Slider.value = fryLevel;
    }

    public void setMusicVolue(float level){
        m_Mixer.SetFloat("MusicVolume", level);
        
    }

    public void setSfxVolue(float level){
        m_Mixer.SetFloat("SfxVolume", level);
        sfxSlider.value = level;
    }

    public void setWalkingVolue(float level){
        m_Mixer.SetFloat("Walk", level);
        walkingSlider.value = level;
    }

    public void setFryingVolue(float level){
        m_Mixer.SetFloat("Fry", level);
        frying_Slider.value = level;
    }

}
