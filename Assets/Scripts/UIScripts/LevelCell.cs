using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNameTextField;
    [SerializeField] private Button _levelButton;
    [SerializeField] private TMP_Text _requiredScore;
    public LevelData _levelDataSO;
    public bool Setup(LevelData levelData)
    {
        _levelDataSO = levelData;
        if (_levelDataSO.scene == null)
        {
            Debug.LogWarning("Scene reference is incorrect in level" + _levelDataSO.levelName);
            return false; // if empty then setup failed
        }

        if (_levelDataSO.levelName != null)
        {
            _levelNameTextField.SetText(_levelDataSO.levelName);
        }
        if (_levelDataSO.thumbnail != null)
        {
            _levelButton.image.sprite = _levelDataSO.thumbnail;
        }
        if (_requiredScore != null)
        {
            _requiredScore.SetText(_levelDataSO.unlockScoreRequirement.ToString());
        }

        return true;
    }
    public void OnLevelClicked()
    {
        LevelManager.Instance.LoadLevel(_levelDataSO);
    }
}
