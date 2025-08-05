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

    public void CheckButtonDo()
    {
        GameManager.Instance.GoGameScene();
    }

    public void ExitButtonDo() {
        SaveManager.Instance.DeleteData(dataName);
        UIManager.Instance.SaveUIOff();
    }
}
