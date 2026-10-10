using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInspectUI : MonoBehaviour
{
    public GameObject panel;
    public Image itemImage;
    public TextMeshProUGUI itemDescription;
    public Button closeButton;

    void Awake()
    {
        panel.SetActive(false);
        closeButton.onClick.AddListener(Hide);
    }

    public void Show(Item item)
    {
        panel.SetActive(true);
        itemImage.sprite = item.icon;
        itemDescription.text = item.itemName + "\n\n" + item.description;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}