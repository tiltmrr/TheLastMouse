using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float sneakSpeed = 1.5f;
    public bool isSneaking = false;
    public InventorySystem inventory;
    public PolygonCollider2D walkableArea;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private Animator animator;

    void Start()
    {
 
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        animator.enabled = false;
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
            MoveTo(mousePos);
            isSneaking = false;
        }
        if (mouse.rightButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            MoveTo(mousePos);
            isSneaking = true;
        }
        if (isMoving)
        {
            float speed = isSneaking ? sneakSpeed : moveSpeed;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
            {
                isMoving = false;
                animator.enabled = false; // остановить анимацию
            }
        }
    }

    public void MoveTo(Vector2 target)
    {
        if (walkableArea != null && walkableArea.OverlapPoint(target))
        {
            targetPosition = target;
            isMoving = true;
            animator.enabled = true; // запустить анимацию
        }
    }

    public void SetSneak(bool sneak)
    {
        isSneaking = sneak;
    }

    public void Interact(GameObject target) { }
}