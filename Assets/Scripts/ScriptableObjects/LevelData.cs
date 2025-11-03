using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "New Level", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    public string levelName = "New Level";
    [Header("Scene & Visuals")]
    public AssetReference scene;
    public Sprite thumbnail;
}
