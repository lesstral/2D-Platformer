using UnityEngine;

[CreateAssetMenu(fileName = "CreditsData", menuName = "Scriptable Objects/CreditsData")]
public class CreditsData : ScriptableObject
{
    public string assetName;
    public string author;
    public string license;
    public string sourceURL;
}
