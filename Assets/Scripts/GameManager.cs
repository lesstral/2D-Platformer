using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _spawnPoint;

    
    [SerializeField] private int _lives = 3;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Game manager already exists");
            Destroy(gameObject);
        }
    }

}
