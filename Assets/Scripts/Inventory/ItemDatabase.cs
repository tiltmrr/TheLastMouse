using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public Item[] allItems;

    public Item GetItemByName(string name)
    {
        foreach (Item item in allItems)
        {
            if (item.itemName == name)
                return item;
        }
        return null;
    }
}