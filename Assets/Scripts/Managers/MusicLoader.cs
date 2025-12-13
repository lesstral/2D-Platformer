using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class MusicLoader : MonoBehaviour
{
    public static MusicLoader Instance;
    public bool _musicLoaded = false;
    private List<MusicCollection> _musicCollections = new();
    private string _addressablesLabel = "MusicCollections";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadMusic();

    }
    private async void LoadMusic()
    {
        var handle = Addressables.LoadAssetsAsync<MusicCollection>(
            _addressablesLabel,
            collection => _musicCollections.Add(collection)
        );

        await handle.Task;
        _musicLoaded = true;

    }
    public List<MusicCollection> GetMusicCollections()
    {
        return _musicCollections;
    }
}
