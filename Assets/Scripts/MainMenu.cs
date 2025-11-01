using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _mainMenu;
    public void OnPlayButtonClicked()
    {
        Debug.Log("Clicked");

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
