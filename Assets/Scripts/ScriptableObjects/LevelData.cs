using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "New Level", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    public string levelName = "New Level";
    [Header("Level UNIQUE ID. Wont show if its occupied. Use for setting level progression")]
    public int ID = 0;
    [Header("Scene & Visuals")]
    public AssetReference scene;
    public Sprite thumbnail;
    [Header("Default is Relaxed")]
    public MusicMood musicMood = MusicMood.Relaxed;
}
