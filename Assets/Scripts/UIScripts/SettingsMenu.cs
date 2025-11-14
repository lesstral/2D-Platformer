using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private bool InGame;
    private List<Resolution> _resolutions = new List<Resolution>();
    List<string> _resolutionOptions = new List<String>();

    private void Start()
    {

        _sliderMaster.value = SettingsManager.Instance._currentMaster;
        _sliderSFX.value = SettingsManager.Instance._currentSFX;
        _sliderMusic.value = SettingsManager.Instance._currentMusic;
        _resolutions = new List<Resolution>(Screen.resolutions);


        if (!InGame)
        {
            int i = 0;
            int currentResolutionIndex = 0;
            foreach (Resolution res in _resolutions)
            {
                _resolutionOptions.Add($"{res.width}x{res.height}");
                i++;
                if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_resolutionOptions);
            _dropdown.value = currentResolutionIndex;
            _dropdown.RefreshShownValue();
        }
    }
    public void BackToMenu()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);

    }
    public void OnMasterChange(float value)
    {
        SettingsManager.Instance.SetVolume(value, AudioChannel.Master);
    }
    public void OnSFXChange(float value)
    {
        SettingsManager.Instance.SetVolume(value, AudioChannel.SFX);
    }
    public void OnMusicChange(float value)
    {
        SettingsManager.Instance.SetVolume(value, AudioChannel.Music);
    }
    public void OnResolutionChange()
    {

    }
}
