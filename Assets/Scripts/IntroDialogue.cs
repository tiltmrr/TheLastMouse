using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class IntroDialogue : MonoBehaviour
{
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public RectTransform bubbleRect;
    public Transform playerTransform;
    public Vector2 bubbleOffset = new Vector2(100f, 150f);
    public GameObject[] blockedExits; // перетащи сюда Window, ExitToKitchen, ExitToOffice

    private string[] lines = {
        "Наконец-то! Последнее уравнение решено...",
        "Хорошо бы доску вытереть, пока мама не увидела.",
        "Странно... Родители ушли по делам ещё утром.",
        "Уже вечер, а их всё нет...",
        "*В окне мелькает что-то странное*"
    };

    private int currentIndex = 0;
    private bool isShowing = false;

    void Start()
    {
        if (GameProgress.IsIntroDone())
        {
            introPanel.SetActive(false);
            SetExits(true);
            return;
        }

        SetExits(false);
        isShowing = true;
        introPanel.SetActive(true);
        ShowLine();
    }

    void Update()
    {
        if (!isShowing) return;

        if (playerTransform != null && bubbleRect != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(playerTransform.position);
            bubbleRect.position = screenPos + bubbleOffset;
        }

        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            currentIndex++;
            if (currentIndex < lines.Length)
                ShowLine();
            else
                FinishIntro();
        }
    }

    private void ShowLine()
    {
        introText.text = lines[currentIndex];
    }

    private void FinishIntro()
    {
        isShowing = false;
        introPanel.SetActive(false);
        GameProgress.MarkIntroDone();
        StartCoroutine(UnblockNextFrame());
    }

    private IEnumerator UnblockNextFrame()
    {
        yield return null;
        SetExits(true);
    }

    private void SetExits(bool active)
    {
        foreach (var exit in blockedExits)
            if (exit != null) exit.SetActive(active);
    }
}