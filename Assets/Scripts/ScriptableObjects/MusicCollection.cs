using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicCollection", menuName = "Scriptable Objects/MusicCollection")]
public class MusicCollection : ScriptableObject
{

    public MusicMood musicMood;
    [SerializeField] public List<AudioClip> audioClips;
}
