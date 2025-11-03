using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    [SerializeField] private AudioMixer _audioMixer;

    public float _currentVolume { get; private set; }
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
    public void SetMasterVolume(float volume)
    {
        _currentVolume = volume;
        if (volume < 0.01)
        {

            _audioMixer.SetFloat("Master", -80);
            return;
        }
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);

    }
    public void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, true);
        _currentResolution = resolution;
    }
    private void LoadSettings()
    {
        _currentVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMasterVolume(_currentVolume);
        int width = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
        _currentResolution = new Resolution { width = width, height = height };
        SetResolution(_currentResolution);
    }
    private void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionWidth", _currentResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", _currentResolution.height);
        PlayerPrefs.SetFloat("MasterVolume", _currentVolume);
        PlayerPrefs.Save();
    }
}


