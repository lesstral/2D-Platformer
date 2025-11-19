using UnityEngine;

public class MainMenu : MonoBehaviour
{
    MainMenu Instance;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelMenu;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Main menu already exists");
            Destroy(gameObject);
        }
    }

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
