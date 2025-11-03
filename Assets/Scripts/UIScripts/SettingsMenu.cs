using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _mainMenu;
    private List<Resolution> _resolutions = new List<Resolution>();
    List<string> _resolutionOptions = new List<String>();
    private void Start()
    {
        _slider.value = SettingsManager.Instance._currentVolume;
        _resolutions = new List<Resolution>(Screen.resolutions);
        foreach (Resolution res in _resolutions)
        {
            _resolutionOptions.Add($"{res.width}x{res.height}");
        }
        _dropdown.ClearOptions();
        _dropdown.AddOptions(_resolutionOptions);
    }
    public void BackToMenu()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);

    }
    public void OnSliderChange(float value)
    {
        SettingsManager.Instance.SetMasterVolume(value);
    }
}
