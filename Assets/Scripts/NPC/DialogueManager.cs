using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject bubblePanel;
    public GameObject cutscenePanel;
    public TextMeshProUGUI bubbleText;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI playerText;
    public GameObject playerTextPanel;
    public RectTransform bubbleRect;

    private DialogueLine[] currentLines;
    private int currentIndex = 0;
    public bool IsActive { get; private set; } = false;
    private float advanceDelay = 0f; // таймер защиты

    void Start()
    {
        dialoguePanel.SetActive(false);
        playerTextPanel.SetActive(false);
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        currentLines = lines;
        currentIndex = 0;
        IsActive = true;
        advanceDelay = 0.2f; // 200мс защита от первого клика
        dialoguePanel.SetActive(true);
        if (cutscenePanel != null) cutscenePanel.SetActive(true);
        ShowLine();
    }

    private void ShowLine()
    {
        DialogueLine line = currentLines[currentIndex];

        bubblePanel.SetActive(false);
        playerTextPanel.SetActive(false);

        if (line.isPlayer)
        {
            playerTextPanel.SetActive(true);
            playerText.text = line.text;
        }
        else
        {
            bubblePanel.SetActive(true);
            speakerName.text = line.speakerName;
            bubbleText.text = line.text;
            bubbleRect.anchoredPosition = line.bubblePosition;
        }
    }

    private void EndDialogue()
    {
        IsActive = false;
        advanceDelay = 0f;
        dialoguePanel.SetActive(false);
        playerTextPanel.SetActive(false);
        if (cutscenePanel != null) cutscenePanel.SetActive(false);
    }

    void Update()
    {
        if (!IsActive) return;

        if (advanceDelay > 0f)
        {
            advanceDelay -= Time.deltaTime;
            return; // ждём пока таймер не спадёт
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.GetComponent<NPC>() != null) return;

            currentIndex++;
            if (currentIndex < currentLines.Length)
                ShowLine();
            else
                EndDialogue();
        }
    }
}