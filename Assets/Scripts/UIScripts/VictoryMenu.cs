using TMPro;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreField;
    [SerializeField] Transform _nextLevelPlacePoint;
    [SerializeField] GameObject _levelCellPrefab;
    [SerializeField] GameObject _levelCellLockedPrefab;
    [SerializeField] GameObject _visualParent;
    private LevelData _nextLevelData;
    private bool _nextLevelLocked;
    private void Awake()
    {


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
        if (_nextLevelData != null)
        {
            SpawnCell(_nextLevelData, score);
        }


    }
    private void SpawnCell(LevelData level, int score)
    {
        RectTransform levelRect;
        if (_nextLevelData.unlockScoreRequirement <= score)
        {
            GameObject levelCell = Instantiate(_levelCellPrefab);
            levelCell.GetComponent<LevelCell>().Setup(level);
            levelRect = levelCell.GetComponent<RectTransform>();

        }
        else
        {
            GameObject levelCell = Instantiate(_levelCellLockedPrefab);
            levelCell.GetComponent<LevelCell>().Setup(level);
            levelRect = levelCell.GetComponent<RectTransform>();
            _nextLevelLocked = true;
        }

        levelRect.SetParent(_nextLevelPlacePoint.transform, false);
    }
    public void OnMenuButtonClicked()
    {

        LevelManager.Instance.LoadMainMenu();
    }
    public void OnNextLevelButtonClicked()
    {
        if (_nextLevelLocked) return;
        LevelManager.Instance.LoadLevel(_nextLevelData);
    }
}
