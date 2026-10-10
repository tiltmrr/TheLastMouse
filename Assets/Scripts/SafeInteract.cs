using UnityEngine;
using UnityEngine.InputSystem;

public class SafeInteract : MonoBehaviour
{
    public Sprite openSprite;
    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        DialogueManager dialogue = FindFirstObjectByType<DialogueManager>();
        if (dialogue != null && dialogue.IsActive) return;

        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!isOpen)
                {
                    // запустить головоломку
                    Puzzle puzzle = FindFirstObjectByType<Puzzle>();
                    if (puzzle != null) puzzle.StartPuzzle();
                }
            }
        }
    }

    public void OpenSafe()
    {
        isOpen = true;
        if (openSprite != null)
            spriteRenderer.sprite = openSprite;
    }
}