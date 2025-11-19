using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelCatalog", menuName = "Scriptable Objects/LevelCatalog")]
public class LevelCatalog : ScriptableObject
{
    public List<LevelData> levelList;
    public static LevelCatalog Instance;
}
