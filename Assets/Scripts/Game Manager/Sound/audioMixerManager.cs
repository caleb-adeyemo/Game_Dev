using UnityEngine;
using UnityEngine.Audio;

public class audioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_Mixer;

    public void setMusicVolue(float level){
        m_Mixer.SetFloat("MusicVolume", level);
    }

    public void setSfxVolue(float level){
        m_Mixer.SetFloat("SfxVolume", level);
    }
}
