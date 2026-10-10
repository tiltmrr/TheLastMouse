using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string text;
    public bool isPlayer;
    public Vector2 bubblePosition;
}

public class NPC : MonoBehaviour
{
    public string npcName;
    public Sprite portrait;
    public DialogueLine[] dialogueLines;
    public bool oneTime = true;
    public string saveKey = "";

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (oneTime && saveKey != "" && GameProgress.IsEventDone(saveKey)) return;

                DialogueManager dialogue = FindFirstObjectByType<DialogueManager>();
                if (dialogue != null && !dialogue.IsActive)
                {
                    if (oneTime && saveKey != "")
                        GameProgress.MarkEventDone(saveKey);
                    dialogue.StartDialogue(dialogueLines); // без корутины
                }
            }
        }
    }
}