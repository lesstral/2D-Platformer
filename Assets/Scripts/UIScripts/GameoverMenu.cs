using UnityEngine;

public class GameoverMenu : MonoBehaviour
{
    [SerializeField] GameObject _visualParent;
    private void Awake()
    {
        _visualParent.SetActive(false);
    }
    private void Start()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("No LevelManager instance available");
        }
    }
    private void OnEnable()
    {
        Events.UIEvents.onGameOver.Subscribe(OnGameover);
    }
    private void OnDisable()
    {
        Events.UIEvents.onGameOver.Unsubscribe(OnGameover);
    }
    private void OnGameover()
    {
        _visualParent.SetActive(true);
    }
    public void OnMainMenuButtonClicked()
    {
        LevelManager.Instance.LoadMainMenu();
    }
    public void OnRetryButtonClicked()
    {
        LevelManager.Instance.ReloadCurrentScene();
    }
}
