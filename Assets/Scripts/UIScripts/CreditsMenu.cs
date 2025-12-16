using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] private List<CreditsData> _creditsDataList;
    [SerializeField] private GameObject _contentParent;
    [SerializeField] private GameObject _creditsPrefab;
    [SerializeField] private GameObject _creditsMenuFrame;
    [SerializeField] private MainMenu _mainMenu;

    private void Start()
    {
        FillLevelGrid();
    }
    private void FillLevelGrid()
    {
        foreach (CreditsData credit in _creditsDataList)
        {
            GameObject creditCell = Instantiate(_creditsPrefab);
            creditCell.GetComponent<CreditsDataCell>().Setup(credit);
            RectTransform levelRect = creditCell.GetComponent<RectTransform>();
            levelRect.SetParent(_contentParent.transform, false);
        }
    }
    public void OnExitButtonClicked()
    {
        this.Close();
        _mainMenu.Open();
    }
    public void Open()
    {
        _creditsMenuFrame.SetActive(true);
    }
    public void Close()
    {
        _creditsMenuFrame.SetActive(false);
    }
}
