using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    [SerializeField] private AudioMixer _audioMixer;

    public float _currentMaster { get; private set; }
    public float _currentSFX { get; private set; }
    public float _currentMusic { get; private set; }
    public Resolution _currentResolution { get; private set; }
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Debug.LogWarning("Settings manager already exists");
            Destroy(gameObject);
        }


    }
    public void SetVolume(float volume, AudioChannel audioChannel)
    {
        string channelString = " ";
        switch (audioChannel)
        {
            case AudioChannel.Master:
                channelString = "Master";
                _currentMaster = volume;
                break;
            case AudioChannel.SFX:
                channelString = "SFX";
                _currentSFX = volume;
                break;
            case AudioChannel.Music:
                channelString = "Music";
                _currentMusic = volume;
                break;
        }

        if (volume < 0.01)
        {

            _audioMixer.SetFloat(channelString, -80);
            return;
        }

        _audioMixer.SetFloat(channelString, Mathf.Log10(volume) * 20);
        SaveSettings();
    }
    public void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, true);
        _currentResolution = resolution;
        SaveSettings();
    }
    private void LoadSettings()
    {
        _currentMaster = PlayerPrefs.GetFloat("Master", 0.5f);
        _currentSFX = PlayerPrefs.GetFloat("SFX", 1f);
        _currentMusic = PlayerPrefs.GetFloat("Music", 1f);
        SetVolume(_currentMaster, AudioChannel.Master);
        SetVolume(_currentSFX, AudioChannel.SFX);
        SetVolume(_currentMusic, AudioChannel.Music);
        int width = PlayerPrefs.GetInt("ResolutionWidth", 800);
        int height = PlayerPrefs.GetInt("ResolutionHeight", 600);
        if (width < 200 || height < 200)
        {
            Debug.LogWarning("bad resolution setting");
            Display display = Display.displays[0];
            width = display.systemWidth;
            height = display.systemHeight;
        }
        _currentResolution = new Resolution { width = width, height = height };
        SetResolution(_currentResolution);
    }
    private void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionWidth", _currentResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", _currentResolution.height);
        PlayerPrefs.SetFloat("Master", _currentMaster);
        PlayerPrefs.SetFloat("SFX", _currentSFX);
        PlayerPrefs.SetFloat("Music", _currentMusic);
        PlayerPrefs.Save();
    }
}


