using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
    public string itemName = "Ключ";
    public string description = "Старый ржавый ключ. Возможно подойдёт к какому-то замку.";
    public Sprite icon;
    public bool isCollectable = true;
    void Start()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);
            if (data.collectedItems != null)
            {
                foreach (string name in data.collectedItems)
                {
                    if (name == itemName)
                    {
                        gameObject.SetActive(false);
                        return;
                    }
                }
            }
        }
    }
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        // не реагируем пока идёт диалог или катсцена
        DialogueManager dialogue = FindFirstObjectByType<DialogueManager>();
        if (dialogue != null && dialogue.IsActive) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Кликнули на: " + itemName);
                InventorySystem inventory = FindFirstObjectByType<InventorySystem>();
                if (inventory != null)
                {
                    inventory.AddItem(this);
                }
            }
        }
    }
}