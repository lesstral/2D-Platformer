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
    [SerializeField] private LevelCatalog _levelCatalogSO;
    private List<LevelData> _levelDataSO = new List<LevelData>();
    private string _addressablesLabel = "LevelData";
    private void Start()
    {
        if (GlobalStateManager.Instance == null)
        {
            Debug.LogError("No GlobalStateManager instance available");
        }
        LevelCatalog.Instance = _levelCatalogSO;
        LoadLevelData();

    }
    private void LoadLevelData()
    {
        Addressables.LoadAssetsAsync<LevelData>(_addressablesLabel, null).Completed += OnLevelsLoaded;
    }
    private void OnLevelsLoaded(AsyncOperationHandle<IList<LevelData>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _levelDataSO = new List<LevelData>(handle.Result);
            Debug.Log($"Loaded {_levelDataSO.Count} levels");
            _levelDataSO.Sort((levelA, levelB) => levelA.ID.CompareTo(levelB.ID));
            _levelCatalogSO.levelList = new List<LevelData>(handle.Result);
            FillLevelGrid();
        }
        else
        {
            Debug.LogError("Failed to load level data");
        }
    }
    private void FillLevelGrid()
    {
        foreach (LevelData level in _levelDataSO)
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
