using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//������ �������� 
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
    public int stageNumber;
    public Player player;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    Player playerData;
    List<InventoryItem> invItemsData;
    SoundManager sound;

    private string path;
    [SerializeField] private string eternalName = "eternalData.json";
    [SerializeField] private string volatileName = "volatileData.json";

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(GameManager.Instance.player != null) // �ӽ�
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

        //TEst
        if (HasData(volatileName))
        {
            Debug.Log("���̺� ���� ����, ������ �ҷ�����");
            if (UIManager.Instance != null)
                UIManager.Instance.SaveUIOn();
            else
                Debug.Log("UI�Ŵ����� ���� ����");
        }
        else
        {
            Debug.Log("���̺� ���� ����");
        }
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

    private void SaveVolatileData()
    {
        playerData = GameManager.Instance.player;
        if (playerData == null) {
            Debug.LogError("playerData is Null");
            return;
        }

        VolatileData volatileData = new();
        volatileData.player = playerData;
        volatileData.stageNumber = GameManager.Instance.currentStage;

        string data = JsonUtility.ToJson(volatileData, true);
        File.WriteAllText(path + volatileName, data);
        Debug.Log("Volatile Data Save");
    }

    public void LoadVolatileData()
    {
        if (HasData(volatileName))
        {
            string data = File.ReadAllText(path + volatileName);

            VolatileData volatileData = new VolatileData();
            JsonUtility.FromJsonOverwrite(data, volatileData);

            playerData = volatileData.player;
            GameManager.Instance.currentStage = volatileData.stageNumber;

            if(data == null)
            {
                Debug.Log("�ֹ߼� �����Ͱ� ����");
                return;
            }
            Debug.Log("Volatile Data Load");
        }
        else
        {
            Debug.LogError("No Data Found");
        }
    }

    private void SaveEternalData()
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

    private void LoadEternalData()
    {
        if (HasData(eternalName))
        {
            string data = File.ReadAllText(path + eternalName);

            EternalData eternalData = new();
            JsonUtility.FromJsonOverwrite(data, eternalData);

            if (eternalData == null)
            {
                Debug.Log("���� �����Ͱ� ����");
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
            Debug.Log(path + fileName);
        }
    }

    public bool HasData(string fileName)
    {
        string pathFull = path + fileName;

        return File.Exists(pathFull);
    }

    internal void SaveGame()
    {
        SaveEternalData();
    }

    internal void SaveStage()
    {
        SaveVolatileData();
    }

    internal void DeleteStage()
    {
        DeleteData(volatileName);
    }
}
