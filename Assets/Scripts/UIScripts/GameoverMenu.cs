using UnityEngine;

public class GameoverMenu : MonoBehaviour
{
    [SerializeField] GameObject _visualParent;
    private void Awake()
    {
        _visualParent.SetActive(false);
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
        GlobalStateManager.Instance.LoadMainMenu();
    }
    public void OnRetryButtonClicked()
    {
        GlobalStateManager.Instance.ReloadCurrentScene();
    }
}
