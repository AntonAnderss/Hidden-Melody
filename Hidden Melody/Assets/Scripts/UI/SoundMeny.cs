using UnityEngine;
using UnityEngine.Audio;

/*
 * Author: Clara Lönnkrans
 *
 *
 *
*/
public class SoundMeny : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
        
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
    public void SetPlayerVolume(float volume)
    {
        audioMixer.SetFloat("PlayerVolume", Mathf.Log10(volume) * 20);
    }

}
