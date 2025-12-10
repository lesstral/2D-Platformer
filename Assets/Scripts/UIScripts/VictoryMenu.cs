using TMPro;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreField;
    [SerializeField] Transform _nextLevelPlacePoint;
    [SerializeField] GameObject _levelCellPrefab;
    [SerializeField] GameObject _visualParent;
    private LevelData _nextLevelData;
    private void Awake()
    {
        _nextLevelData = GlobalStateManager.Instance.GetNextLevelData();
        if (_nextLevelData != null)
        {
            SpawnCell(_nextLevelData);
        }
        _visualParent.SetActive(false);
    }
    private void OnEnable()
    {
        Events.UIEvents.onVictory.Subscribe(OnVictory);
    }
    private void OnDisable()
    {
        Events.UIEvents.onVictory.Unsubscribe(OnVictory);
    }
    private void OnVictory(int score)
    {
        _scoreField.SetText(score.ToString());
        _visualParent.SetActive(true);
    }
    private void SpawnCell(LevelData level)
    {
        GameObject levelCell = Instantiate(_levelCellPrefab);
        levelCell.GetComponent<LevelCell>().Setup(level);
        RectTransform levelRect = levelCell.GetComponent<RectTransform>();
        levelRect.SetParent(_nextLevelPlacePoint.transform, false);
    }
    public void OnMenuButtonClicked()
    {
        GlobalStateManager.Instance.LoadMainMenu();
    }
    public void OnNextLevelButtonClicked()
    {
        GlobalStateManager.Instance.LoadLevel(_nextLevelData);
    }
}
