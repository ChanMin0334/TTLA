using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Gold;
    private string dataName = "volatileData.json";

    public Button checkButton;
    public Button exitButton;

    private void Awake()
    {
        checkButton = GetComponentInChildren<Button>(true);
        exitButton = GetComponentInChildren<Button>(true);

        checkButton.onClick.AddListener(CheckButtonDo);
        exitButton.onClick.AddListener(ExitButtonDo);
    }

    public void CheckButtonDo()
    {
        GameManager.Instance.GoGameScene();
    }

    public void ExitButtonDo() {
        SaveManager.Instance.DeleteData(dataName);
        UIManager.Instance.SaveUIOff();
    }
}
