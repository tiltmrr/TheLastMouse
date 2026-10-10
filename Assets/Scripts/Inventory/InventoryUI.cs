using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public float animationSpeed = 5f;
    public Image[] slots; // 6 слотов

    private RectTransform panelRect;
    private Vector2 hiddenPosition;
    private Vector2 shownPosition;
    public bool isOpen = false;
    public static InventoryUI Instance { get; private set; }
    public string SelectedItem { get; private set; } = null;
    public Color selectedColor = new Color(1f, 0.85f, 0.2f, 1f);
    private int selectedSlotIndex = -1;

    void Awake()
    {
        panelRect = inventoryPanel.GetComponent<RectTransform>();
        shownPosition = panelRect.anchoredPosition;
        hiddenPosition = new Vector2(shownPosition.x, -600f);
        panelRect.anchoredPosition = hiddenPosition;

        // очистить все слоты
        foreach (Image slot in slots)
        {
            slot.sprite = null;
            slot.color = new Color(1, 1, 1, 0);
        }
        Instance = this;
        SetupSlotClicks();
    }

    void Update()
    {
        Vector2 target = isOpen ? shownPosition : hiddenPosition;
        float distance = Vector2.Distance(panelRect.anchoredPosition, target);
        float speed = animationSpeed * distance;
        panelRect.anchoredPosition = Vector2.MoveTowards(panelRect.anchoredPosition, target, speed * Time.deltaTime);
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
    }

    public void AddItemToSlot(Sprite icon)
    {
        foreach (Image slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.sprite = icon;
                slot.color = Color.white;
                return;
            }
        }
    }
    public void ClearSlots()
    {
        foreach (Image slot in slots)
        {
            slot.sprite = null;
            slot.color = new Color(1, 1, 1, 0);
        }
    }

    private void SetupSlotClicks()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int index = i;
            Button btn = slots[i].gameObject.GetComponent<Button>();
            if (btn == null) btn = slots[i].gameObject.AddComponent<Button>();
            btn.transition = Selectable.Transition.None;
            btn.onClick.AddListener(() => OnSlotClicked(index));
        }
    }

    private void OnSlotClicked(int index)
    {
        if (slots[index].sprite == null || slots[index].color.a < 0.1f) return;
        if (selectedSlotIndex == index) { Deselect(); return; }
        Deselect();
        selectedSlotIndex = index;
        slots[index].color = selectedColor;
        InventorySystem inv = FindFirstObjectByType<InventorySystem>();
        if (inv != null && index < inv.items.Count)
            SelectedItem = inv.items[index].itemName;
    }

    public void Deselect()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Length)
            slots[selectedSlotIndex].color = Color.white;
        selectedSlotIndex = -1;
        SelectedItem = null;
    }
}