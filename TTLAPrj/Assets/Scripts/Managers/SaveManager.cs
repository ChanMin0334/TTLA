using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//데이터 나눠야함 
[System.Serializable]
class EternalData
{
    public List<InventoryItem> invItems;
    public float sfxVolume;
    public float bgmVolume;
}

[System.Serializable]
class VolatileData
{
    //public float stageNumber;
    public Player player;
}

public class SaveManager : MonoBehaviour
{
    Player playerData;
    List<InventoryItem> invItemsData;
    SoundManager sound;

    private string path;
    [SerializeField] private string eternalName = "eternalData.json";
    [SerializeField] private string volatileName = "volatileData.json";

    private void Start()
    {
        if(GameManager.Instance != null) // 임시
        {
            playerData = GameManager.Instance.player;
        }

        if(ItemManager.Instance != null)
        {
            invItemsData = ItemManager.Instance.inventory;
        }

        if(SoundManager.Instance != null)
        {
            sound = SoundManager.Instance;
        }

        path = Application.persistentDataPath + "/";
        Debug.Log(path);
    }

    public void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            SaveEternalData();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            LoadEternalData();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            SaveVolatileData();
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            LoadVolatileData();
        }
    }

    public void SaveVolatileData()
    {
        if (playerData == null) {
            Debug.LogError("playerData is Null");
            return;
        }

        VolatileData volatileData = new();
        volatileData.player = playerData;

        string data = JsonUtility.ToJson(volatileData, true);
        File.WriteAllText(path + volatileName, data);
        Debug.Log("Volatile Data Save");
    }

    public void LoadVolatileData()
    {
        if (HasData(volatileName))
        {
            string data = File.ReadAllText(path + volatileName);

            JsonUtility.FromJsonOverwrite(data, playerData);

            if(data == null)
            {
                Debug.Log("휘발성 데이터가 없음");
                return;
            }
            Debug.Log("Volatile Data Load");
        }
        else
        {
            Debug.LogError("No Data Found");
        }
    }

    public void SaveEternalData()
    {
        if (invItemsData == null)
        {
            Debug.LogError("invItemsData is Null");
            return;
        }

        EternalData eternalData = new();
        eternalData.invItems = invItemsData;
        eternalData.bgmVolume = sound.bgmVolume;
        eternalData.sfxVolume = sound.sfxVolume;

        string data = JsonUtility.ToJson(eternalData, true);
        File.WriteAllText(path + eternalName, data);
        Debug.Log("Eternal Data Save");
    }

    public void LoadEternalData()
    {
        if (HasData(eternalName))
        {
            string data = File.ReadAllText(path + eternalName);

            EternalData eternalData = new();
            JsonUtility.FromJsonOverwrite(data, eternalData);

            if (eternalData == null)
            {
                Debug.Log("영구 데이터가 없음");
                return;
            }

            if (ItemManager.Instance != null) {
                ItemManager.Instance.inventory = eternalData.invItems;
            }

            if (SoundManager.Instance != null) {
                SoundManager.Instance.bgmVolume = eternalData.bgmVolume;
                SoundManager.Instance.sfxVolume = eternalData.sfxVolume;
            }

            Debug.Log("Eternal Data Load");
        }
        else
        {
            Debug.LogError("No Data Found");
        }
    }

    public void DeleteData(string fileName)
    {
        if (HasData(fileName)) {
            File.Delete(path + fileName);
            Debug.Log("Data Delete");
        }
        else
        {
            Debug.LogError("Data Delete Failed");
        }
    }

    public bool HasData(string fileName)
    {
        string pathFull = path + fileName;

        return File.Exists(pathFull);
    }
}
