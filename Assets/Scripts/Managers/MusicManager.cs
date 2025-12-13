using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections.ObjectModel;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioSource _audioSource;
    private List<AudioClip> _currentPlaylist;
    private MusicMood _currentMusicMood;
    private int _currentIndex;
    private int _futureIndex;
    private System.Random _random;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _random = new System.Random();
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogWarning("No LevelManagerInstance, MusicManager inactive");
            gameObject.SetActive(false);
            return;
        }
        if (MusicLoader.Instance == null)
        {
            Debug.LogWarning("No MusicLoaderInstance, MusicManager inactive");
            gameObject.SetActive(false);
            return;
        }
        if (!MusicLoader.Instance._musicLoaded)
        {
            StartCoroutine(WaitForMusicLoadAndStart());
        }


    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
    //good for now, although adds 0.15 ms to frametime
    // TODO: avoid constant polling
    // preload next clip to avoid stutters
    private System.Collections.IEnumerator PlayLoop()
    {
        while (true)
        {

            _currentIndex = _futureIndex;


            AudioClip clip = _currentPlaylist[_currentIndex];
            if (clip == null) yield break;

            _audioSource.clip = clip;
            _audioSource.Play();
            _futureIndex = GenerateRandomIndex(_currentIndex, _currentPlaylist);
            // future index avoids unlikely scenario
            // where random generator outputs previous number
            // again and again interrupting music flow

            float overlap = 0.05f;
            yield return new WaitForSecondsRealtime(clip.length - overlap);


            if (_audioSource.isPlaying)
                yield return new WaitUntil(() => !_audioSource.isPlaying);
        }
    }
    private void OnNewSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        StartMusic();
    }
    private void StartMusic()
    {
        StopMusic();
        GetCurrentMusicMood();
        BuildPlaylist();
        if (_currentPlaylist.Count == 0)
        {
            Debug.LogWarning("No music in playlist");
            return;
        }

        _currentIndex = GenerateRandomIndex(-1, _currentPlaylist);
        _futureIndex = GenerateRandomIndex(_currentIndex, _currentPlaylist);
        StartCoroutine(PlayLoop());
    }
    private void StopMusic()
    {
        _currentIndex = 0;
        _futureIndex = 0;
        StopAllCoroutines();
    }
    private void BuildPlaylist()
    {
        _currentPlaylist = new List<AudioClip>();
        List<MusicCollection> musicCollections =
        MusicLoader.Instance.GetMusicCollections();

        foreach (MusicCollection collection in musicCollections)
        {
            if (collection.musicMood == _currentMusicMood)
            {
                _currentPlaylist.AddRange(collection.audioClips);
            }
        }
    }

    private void GetCurrentMusicMood()
    {

        LevelData currentLevel = LevelManager.Instance.GetCurrentLevelData();
        if (currentLevel != null)
        {
            _currentMusicMood = currentLevel.musicMood;
        }
        else
        {
            _currentMusicMood = MusicMood.Relaxed; //assuming its main menu
        }
    }
    private int GenerateRandomIndex(int previous, List<AudioClip> playlist)
    {

        int clipIndex;
        do
        {
            clipIndex = _random.Next(0, playlist.Count);
        } while (clipIndex == previous);
        return clipIndex;
    }
    private System.Collections.IEnumerator WaitForMusicLoadAndStart()
    {
        yield return new WaitUntil(() => MusicLoader.Instance._musicLoaded);
        StartMusic();
    }

}
