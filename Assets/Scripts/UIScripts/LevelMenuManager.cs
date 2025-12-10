using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _contentParent;
    [SerializeField] private GameObject _levelCellPrefab;
    [SerializeField] private GameObject _levelCellPrefabLocked;

    private void Start()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("No LevelManager instance available");
        }

    }
    private void OnEnable()
    {
        Events.UIEvents.onLevelsLoaded.Subscribe(OnLevelsLoaded);
    }
    private void OnDisable()
    {
        Events.UIEvents.onLevelsLoaded.Unsubscribe(OnLevelsLoaded);
    }
    private void OnLevelsLoaded()
    {
        FillLevelGrid(LevelManager.Instance.GetLoadedLevels());

    }
    private void FillLevelGrid(List<LevelData> levels)
    {
        foreach (LevelData level in levels)
        {
            GameObject levelCell;
            if (!GlobalStateManager.Instance.IsLocked(level.ID) || level.ID == 0)
            {
                levelCell = Instantiate(_levelCellPrefab);
            }
            else
            {
                levelCell = Instantiate(_levelCellPrefabLocked);
            }

            levelCell.GetComponent<LevelCell>().Setup(level);
            RectTransform levelRect = levelCell.GetComponent<RectTransform>();
            levelRect.SetParent(_contentParent.transform, false);
        }
    }
    public void OnExitButtonClicked()
    {
        _levelMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
