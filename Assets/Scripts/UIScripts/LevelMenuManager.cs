using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _contentParent;
    [SerializeField] private GameObject _levelCellPrefab;
    private List<LevelData> levelDataSO = new List<LevelData>();
    private string _addressablesLabel = "LevelData";
    private void Start()
    {
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
            levelDataSO = new List<LevelData>(handle.Result);
            Debug.Log($"Loaded {levelDataSO.Count} levels");
            FillLevelGrid();
        }
        else
        {
            Debug.LogError("Failed to load level data");
        }
    }
    private void FillLevelGrid()
    {
        foreach (LevelData level in levelDataSO)
        {
            GameObject levelCell = Instantiate(_levelCellPrefab);
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
