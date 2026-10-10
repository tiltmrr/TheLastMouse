using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DoorInteract : MonoBehaviour
{
    public GameObject doorCloseup;
    public GameObject doorOpenSprite;
    public string requiredItem = "Ключ";
    public GameObject creditsPanel;
    public GameObject player; // перетащи сюда объект Player в инспекторе

    private bool isCloseup = false;
    private bool doorOpened = false;

    void Update()
    {
        if (doorOpened) return;

        var mouse = Mouse.current;
        if (mouse == null) return;

        DialogueManager dialogue = FindFirstObjectByType<DialogueManager>();
        if (dialogue != null && dialogue.IsActive) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!isCloseup)
                {
                    isCloseup = true;
                    doorCloseup.SetActive(true);
                    if (player != null) player.SetActive(false); // скрываем игрока
                }
                else
                {
                    string selected = InventoryUI.Instance != null ? InventoryUI.Instance.SelectedItem : null;

                    if (selected == requiredItem)
                    {
                        StartCoroutine(OpenDoorAndCredits());
                    }
                    else
                    {
                        Debug.Log("Нужен ключ!");
                    }
                }
            }
        }
    }

    private IEnumerator OpenDoorAndCredits()
    {
        doorOpened = true;

        // убираем ключ из инвентаря
        InventorySystem inv = FindFirstObjectByType<InventorySystem>();
        if (inv != null)
        {
            Item key = inv.items.Find(i => i.itemName == requiredItem);
            if (key != null) inv.RemoveItem(key);
        }

        if (InventoryUI.Instance != null) InventoryUI.Instance.Deselect();

        if (doorCloseup != null) doorCloseup.SetActive(false);
        if (doorOpenSprite != null) doorOpenSprite.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (creditsPanel != null) creditsPanel.SetActive(true);
    }
}