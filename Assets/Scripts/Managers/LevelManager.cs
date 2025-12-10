using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private List<LevelData> _loadedLevels;
    private LevelData _currentLevel;
    private string _addressablesLabel = "LevelData";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLevelData();
    }
    private void LoadLevelData()
    {
        Addressables.LoadAssetsAsync<LevelData>(_addressablesLabel, null).Completed += OnLevelsLoaded;
    }
    private void OnLevelsLoaded(AsyncOperationHandle<IList<LevelData>> handle)
    {
        Debug.Log("levels loading");
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadedLevels = new List<LevelData>(handle.Result);
            _loadedLevels.Sort((levelA, levelB) => levelA.ID.CompareTo(levelB.ID));
            Debug.Log(_loadedLevels.Count);
            Events.UIEvents.onLevelsLoaded.Publish();

        }
        else
        {
            Debug.LogError("Failed to load level data");
        }

    }
    public LevelData GetCurrentLevelData()
    {
        return _currentLevel;
    }
    private void SetCurrentLevel(LevelData newLevel)
    {
        _currentLevel = newLevel;
    }
    public LevelData GetNextLevelData()
    {
        int nextID = _currentLevel.ID + 1;
        return _loadedLevels.Find(levelData => levelData.ID == nextID);
    }
    public void LoadLevel(LevelData levelData)
    {
        levelData.scene.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
        SetCurrentLevel(levelData);
    }
    public List<LevelData> GetLoadedLevels()
    {
        return _loadedLevels;
    }
    public void LoadMainMenu()
    {
        _currentLevel = null;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void ReloadCurrentScene()
    {
        LevelData tempLevelData = _currentLevel;
        UnloadCurrentScene();
        LoadLevel(tempLevelData);
    }
    public void UnloadCurrentScene()
    {
        if (_currentLevel != null)
        {
            if (_currentLevel.scene.OperationHandle.IsValid())
            {
                _currentLevel.scene.UnLoadScene();
                _currentLevel = null;
            }
        }
    }
}
