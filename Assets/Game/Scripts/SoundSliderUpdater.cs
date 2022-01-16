using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoundSliderUpdater : MonoBehaviour
{
    public AudioManager.AudioChannel channel;
    public TMP_Text soundText;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        switch (channel)
        {
            case AudioManager.AudioChannel.Master:
                slider.value = (float)PlayerPrefs.GetInt("MasterVolume") / 100;
                break;
            case AudioManager.AudioChannel.Sound:
                slider.value = (float)PlayerPrefs.GetInt("SoundVolume") / 100;
                break;
            case AudioManager.AudioChannel.Music:
                slider.value = (float)PlayerPrefs.GetInt("MusicVolume") / 100;
                break;
        }
    }

    public void UpdateSoundLevels()
    {
        int sliderValue = (int)(slider.value * 100);

        AudioManager.Instance.SetVolume(channel,sliderValue);

        switch (channel)
        {
            case AudioManager.AudioChannel.Master:
                soundText.text = $"Master Volume: {sliderValue} / 100";
                break;
            case AudioManager.AudioChannel.Sound:
                soundText.text = $"Sound Volume: {sliderValue} / 100";
                break;
            case AudioManager.AudioChannel.Music:
                soundText.text = $"Music Volume: {sliderValue} / 100";
                break;
        }
    }
}
