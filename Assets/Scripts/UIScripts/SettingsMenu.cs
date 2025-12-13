using System;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenuFrame;
    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private bool InGame;
    private List<Resolution> _resolutions = new List<Resolution>();
    List<string> _resolutionOptions = new List<String>();
    int _currentResolutionIndex = 0;

    private void Start()
    {

        _sliderMaster.value = SettingsManager.Instance._currentMaster;
        _sliderSFX.value = SettingsManager.Instance._currentSFX;
        _sliderMusic.value = SettingsManager.Instance._currentMusic;
        _resolutions = new List<Resolution>(Screen.resolutions);


        if (!InGame)
        {
            int i = 0;

            foreach (Resolution res in _resolutions)
            {
                _resolutionOptions.Add($"{res.width}x{res.height}");
                i++;
                if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                {
                    _currentResolutionIndex = i;
                }
            }
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_resolutionOptions);
            _dropdown.value = _currentResolutionIndex;
            _dropdown.RefreshShownValue();
        }
    }
    public void OnBackToMenuButtonClicked()
    {
        this.Close();
        _mainMenu.Open();
    }
    public void Open()
    {
        _settingsMenuFrame.SetActive(true);
    }
    public void Close()
    {
        _settingsMenuFrame.SetActive(false);
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
    public void OnResolutionChange(int value)
    {
        Screen.SetResolution(_resolutions[value].width, _resolutions[value].height, true);
        _currentResolutionIndex = value;

    }
}
