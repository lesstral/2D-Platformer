using UnityEngine;
using System;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MusicCollection _musicCollection;
    private int _previousIndex;
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
        if (_musicCollection.audioClips.Count == 0)
        {
            Debug.LogWarning("MusicCollection is empty");
        }
        else
        {
            _currentIndex = GenerateRandomIndex(-1);
            _futureIndex = GenerateRandomIndex(_currentIndex);
            StartCoroutine(PlayLoop());
        }
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    //good for now, although adds 0.15 ms to frametime
    // TODO: avoid constant polling
    // preload next clip to avoid stutters
    private System.Collections.IEnumerator PlayLoop()
    {
        while (true)
        {

            _previousIndex = _currentIndex;
            _currentIndex = _futureIndex;
            _futureIndex = GenerateRandomIndex(_currentIndex);
            // future index avoids unlikely scenario
            // where random generator outputs previous number
            // again and again interrupting music flow
            AudioClip clip = _musicCollection.audioClips[_currentIndex];
            if (clip == null) yield break;

            _audioSource.clip = clip;
            _audioSource.Play();


            float overlap = 0.05f;
            yield return new WaitForSecondsRealtime(clip.length - overlap);


            if (_audioSource.isPlaying)
                yield return new WaitUntil(() => !_audioSource.isPlaying);
        }
    }
    private int GenerateRandomIndex(int previous)
    {
        int clipIndex;
        do
        {
            clipIndex = _random.Next(0, _musicCollection.audioClips.Count);
        } while (clipIndex == previous);
        return clipIndex;
    }

}
