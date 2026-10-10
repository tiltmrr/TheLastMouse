using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public ItemInspectUI inspectUI;
    public ItemDatabase itemDatabase;

    void Start()
    {
        Load();
    }

    public void AddItem(Item item)
    {
        if (item.isCollectable)
        {
            items.Add(item);

            HintManager hintManager = FindFirstObjectByType<HintManager>();
            if (hintManager != null) hintManager.NextHint();

            InventoryUI inventoryUI = FindFirstObjectByType<InventoryUI>();
            if (inventoryUI != null)
            {
                inventoryUI.isOpen = false;
                inventoryUI.AddItemToSlot(item.icon);
            }

            inspectUI.Show(item);
            item.gameObject.SetActive(false);
            Save();
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Save();
    }

    public void Save()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        string[] itemNames = new string[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            itemNames[i] = items[i].itemName;
        }
        InventorySaveData saveData = new InventorySaveData
        {
            itemNames = itemNames,
            collectedItems = itemNames
        };
        string json = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(path, json);
    }

    public void Load()
    {
        items.Clear();
        InventoryUI inventoryUI = FindFirstObjectByType<InventoryUI>();
        if (inventoryUI != null)
            inventoryUI.ClearSlots();

        string path = Application.persistentDataPath + "/inventory.json";
        if (!System.IO.File.Exists(path)) return;

        string json = System.IO.File.ReadAllText(path);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        foreach (string itemName in data.itemNames)
        {
            Item item = itemDatabase.GetItemByName(itemName);
            if (item != null)
            {
                items.Add(item);
                if (inventoryUI != null)
                    inventoryUI.AddItemToSlot(item.icon);
            }
        }
    }
}

[System.Serializable]
public class InventorySaveData
{
    public string[] itemNames;
    public string[] collectedItems;
}