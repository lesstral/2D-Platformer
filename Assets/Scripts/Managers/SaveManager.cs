using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public PlayerData Data { get; private set; }
    private string savePath;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "player.json");
        Load();
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("No LevelManager instance available");
        }
    }
    private void OnEnable()
    {
        Events.UIEvents.onVictory.Subscribe(UpdateScore);
    }
    private void OnDisable()
    {
        Events.UIEvents.onVictory.Unsubscribe(UpdateScore);
    }
    private void Load()
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
    public void UpdateScore(int score)
    {
        LevelData currentLevel = LevelManager.Instance.GetCurrentLevelData();
        if (Data.bestScores.ContainsKey(currentLevel.ID))
        {
            Data.bestScores[currentLevel.ID] = score;
        }
        else
        {
            Data.bestScores.Add(currentLevel.ID, score);
        }
        Save();
    }
    public bool IsLocked(int levelID)
    {

        return !Data.bestScores.ContainsKey(levelID);
    }
    private void Save()
    {
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(savePath, json);
    }
}
