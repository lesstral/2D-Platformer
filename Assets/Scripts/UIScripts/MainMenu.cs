using UnityEngine;

public class MainMenu : MonoBehaviour
{
    MainMenu Instance;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private CreditsMenu _creditsMenu;
    [SerializeField] private GameObject _mainMenuFrame;
    [SerializeField] private GameObject _settingButtonFrame;
    [SerializeField] private LevelMenuManager _levelMenu;
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
        this.Close();
        _levelMenu.Open();
    }
    public void OnCreditsButtonClicked()
    {
        this.Close();
        _creditsMenu.Open();
    }
    public void OnSettingsButtonClicked()
    {
        this.Close();
        _settingsMenu.Open();
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void Open()
    {
        _mainMenuFrame.SetActive(true);
        _settingButtonFrame.SetActive(true);
    }
    public void Close()
    {
        _mainMenuFrame.SetActive(false);
        _settingButtonFrame.SetActive(false);
    }
}
