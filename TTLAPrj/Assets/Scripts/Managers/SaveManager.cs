using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    Player playerData = GameManager.Instance.player;

    private string path;
    [SerializeField] private string fileName = "save";

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        Debug.Log(path);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadData();
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + fileName, data);
        Debug.Log("Data Save");
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName);
        playerData = JsonUtility.FromJson<Player>(data);
        Debug.Log("Data Load"); 
    }
}
