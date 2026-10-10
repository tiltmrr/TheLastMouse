using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneTransition : MonoBehaviour
{
    public string sceneName;
    public Texture2D hoverCursor;

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
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    void OnMouseEnter()
    {
        if (hoverCursor != null)
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}