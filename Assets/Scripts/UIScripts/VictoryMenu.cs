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

        if (_nextLevelData != null)
        {
            SpawnCell(_nextLevelData);
        }
        _visualParent.SetActive(false);
    }
    private void Start()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("No LevelManager instance available");
        }
        _nextLevelData = LevelManager.Instance.GetNextLevelData();
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
        LevelManager.Instance.LoadMainMenu();
    }
    public void OnNextLevelButtonClicked()
    {
        LevelManager.Instance.LoadLevel(_nextLevelData);
    }
}
