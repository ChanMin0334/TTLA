using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//������ �������� 
[System.Serializable]
class EternalData
{
    public List<InventoryItem> invItems;
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

    private string path;
    [SerializeField] private string eternalName = "eternalData.json";
    [SerializeField] private string volatileName = "volatileData.json";

    private void Start()
    {
        if(GameManager.Instance.player != null) // �ӽ�
        {
            playerData = GameManager.Instance.player;
        }

        if(ItemManager.Instance.inventory != null)
        {
            invItemsData = ItemManager.Instance.inventory;
        }

        path = Application.persistentDataPath + "/";
        Debug.Log(path);
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

            VolatileData volatileData = new();
            JsonUtility.FromJsonOverwrite(data, volatileData);

            if(volatileData == null)
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

    public void SaveEternalData()
    {
        if (invItemsData == null)
        {
            Debug.LogError("invItemsData is Null");
            return;
        }

        EternalData eternalData = new();
        eternalData.invItems = invItemsData;

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
                Debug.Log("���� �����Ͱ� ����");
                return;
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
