using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    Player playerData;

    private string path;
    [SerializeField] private string fileName = "save.json";

    private void Awake()
    {
        if(GameManager.Instance.player != null) // юс╫ц
        {
            playerData = GameManager.Instance.player;
        }
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
        if (playerData == null) {
            Debug.LogError("playerData is Null");
        }

        string data = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path + fileName, data);
        Debug.Log("Data Save");
    }

    public void LoadData()
    {
        if (HasData())
        {
            string data = File.ReadAllText(path + fileName);
            JsonUtility.FromJsonOverwrite(data, playerData);
            Debug.Log("Data Load");
        }
        else
        {
            Debug.LogError("No Data Found");
        }
    }

    public void DeleteData()
    {
        if (HasData()) {
            File.Delete(path + fileName);
            Debug.Log("Data Delete");
        }
        else
        {
            Debug.LogError("Data Delete Failed");
        }
    }

    public bool HasData()
    {
        string pathFull = path + fileName;

        return File.Exists(pathFull);
    }
}
