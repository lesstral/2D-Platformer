using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject[] _spriteLives = new GameObject[3];

    private void OnEnable()
    {
        Events.UIEvents.onLiveCounterUpdate.Add(UpdateCounter);
    }
    private void OnDisable()
    {
        Events.UIEvents.onLiveCounterUpdate.Remove(UpdateCounter);
    }
    private void UpdateCounter(int newLivesCount)
    {
        if (newLivesCount < _lives)
        {
            _spriteLives[_lives - 1].SetActive(false);

        }
        else if (newLivesCount > _lives)
        {
            Debug.Log(newLivesCount - 1);
            _spriteLives[_lives - 1].SetActive(true);
            // need to refactor if it actually becomes that dynamic
        }
        _lives = newLivesCount;
    }
}
