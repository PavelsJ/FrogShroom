using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soumdscript : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    private const string MusicVolumeKey = "musicVolume"; 

    private void Start()
    {
        LoadVolume(); 
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume(); 
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume; 
        }
        else
        {
            volumeSlider.value = 1f; 
        }
    }

    private void SaveVolume()
    {
        float currentVolume = volumeSlider.value;
        PlayerPrefs.SetFloat(MusicVolumeKey, currentVolume);
        PlayerPrefs.Save(); 
    }

}
