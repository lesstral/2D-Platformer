using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _buttonGrid;
    private bool isMenuOpen = false;

    private void OnEnable()
    {
        Events.UIEvents.MenuOnKeyOpen.Add(HandleEvent);
    }
    private void OnDisable()
    {
        Events.UIEvents.MenuOnKeyOpen.Remove(HandleEvent);
    }
    private void HandleEvent()
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
        isMenuOpen = true;
        Events.UIEvents.onMenuOpened.Publish();
    }
    public void ClosePauseMenu()
    {
        Events.UIEvents.onMenuClosed.Publish();
        isMenuOpen = false;
        _pauseMenu.SetActive(false);
        _buttonGrid.SetActive(true);
    }
    public void BackToMainMenu()
    {

    }

}
