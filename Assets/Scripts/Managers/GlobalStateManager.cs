using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance;
    [SerializeField] private LevelCatalog _levelCatalogSO;
    public PlayerData Data { get; private set; }
    private LevelData _currentLevel;
    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "player.json");
            Load();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    public void UpdateScore(int score)
    {
        if (Data.bestScores.ContainsKey(_currentLevel.ID))
        {
            Data.bestScores[_currentLevel.ID] = score;
        }
        else
        {
            Data.bestScores.Add(_currentLevel.ID, score);
        }
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(savePath, json);
    }
    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Data = new PlayerData();
        }
    }
    public bool IsLocked(int levelID)
    {

        return !Data.bestScores.ContainsKey(levelID);
    }
    public LevelData GetCurrentLevelData()
    {
        return _currentLevel;
    }
    public LevelData GetNextLevelData()
    {
        int nextID = _currentLevel.ID + 1;
        return _levelCatalogSO.levelList.Find(levelData => levelData.ID == nextID);
    }
    public void LoadLevel(LevelData levelData)
    {
        levelData.scene.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void SetCurrentLevel(LevelData newLevel)
    {
        _currentLevel = newLevel;
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
