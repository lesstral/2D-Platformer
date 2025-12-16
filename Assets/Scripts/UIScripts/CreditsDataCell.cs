using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsDataCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _assetNameField;
    [SerializeField] private TMP_Text _authorField;
    [SerializeField] private TMP_Text _licenseField;
    private CreditsData _creditsDataSO;

    public void Setup(CreditsData creditsData)
    {
        _creditsDataSO = creditsData;
        _assetNameField.SetText(creditsData.assetName);
        _authorField.SetText(creditsData.author);
        _licenseField.SetText(creditsData.license);
    }
    public void OnCellClicked()
    {
        Application.OpenURL(_creditsDataSO.sourceURL);
    }
}
