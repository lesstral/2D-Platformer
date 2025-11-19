using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : SettingsMenu
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _buttonGrid;
    [SerializeField] private GameObject _livesCounter;
    private bool isMenuOpen = false;

    private void OnEnable()
    {
        Events.UIEvents.menuOnKeyOpen.Add(ToggleMenu);
        Events.UIEvents.onGameOver.Add(OnGameOver);
    }
    private void OnDisable()
    {
        Events.UIEvents.menuOnKeyOpen.Remove(ToggleMenu);
        Events.UIEvents.onGameOver.Remove(OnGameOver);
    }
    private void OnGameOver()
    {
        _pauseMenu.SetActive(false);
        _buttonGrid.SetActive(false);
        _livesCounter.SetActive(false);
    }
    private void ToggleMenu()
    {
        if (isMenuOpen)
        {
            ClosePauseMenu();
        }
        else
        {
            OpenPauseMenu();
        }
    }
    public void OpenPauseMenu()
    {
        _pauseMenu.SetActive(true);
        _buttonGrid.SetActive(false);
        _livesCounter.SetActive(false);
        isMenuOpen = true;
        Events.UIEvents.onMenuOpened.Publish();
    }
    public void ClosePauseMenu()
    {
        Events.UIEvents.onMenuClosed.Publish();
        isMenuOpen = false;
        _pauseMenu.SetActive(false);
        _buttonGrid.SetActive(true);
        _livesCounter.SetActive(true);
    }
    public void BackToMainMenu()
    {
        GlobalStateManager.Instance.LoadMainMenu();
    }

}
