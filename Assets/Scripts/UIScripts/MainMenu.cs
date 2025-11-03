using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelMenu;
    public void OnPlayButtonClicked()
    {
        _mainMenu.SetActive(false);
        _levelMenu.SetActive(true);
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);

    }
}
