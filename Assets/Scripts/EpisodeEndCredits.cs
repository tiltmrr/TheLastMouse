using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

/// <summary>
/// Финальная сцена эпизода 1.
/// Чёрный фон, текст появляется с fade, клик — следующая фраза,
/// после последней — главное меню.
/// 
/// Настройка Canvas:
///  - Panel (черный Image, alpha=1) → назови BlackPanel
///  - TextMeshPro — назови EpisodeText
///  - Повесь этот скрипт на любой GameObject
///  - Перетащи поля в инспекторе
/// </summary>
public class EpisodeEndCredits : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasGroup;       // на корневом объекте Canvas или Panel
    public TextMeshProUGUI episodeText;   // текст поверх чёрного

    [Header("Настройки")]
    public float fadeDuration = 1.2f;    // время появления каждой фразы
    public string mainMenuScene = "MainMenu";

    private string[] lines = {
        "Он сделал это.",
        "Маленький мышонок по имени Эдгар\nвпервые в жизни ослушался.",
        "Документы были у него.\nКоты остались ни с чем.",
        "Но впереди был лагерь.\nИ то, что он там найдёт,\nизменит всё.",
        "Конец эпизода 1"
    };

    private int currentIndex = 0;
    private bool isAnimating = false;
    private bool finished = false;

    void Start()
    {
        episodeText.text = "";
        episodeText.color = new Color(1, 1, 1, 0);
        StartCoroutine(ShowLine());
    }

    void Update()
    {
        if (finished || isAnimating) return;

        var mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
            NextLine();

    }

    private void NextLine()
    {
        currentIndex++;
        if (currentIndex < lines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            finished = true;
            StartCoroutine(GoToMenu());
        }
    }

    private IEnumerator ShowLine()
    {
        isAnimating = true;

        // Fade out старого текста
        yield return StartCoroutine(FadeText(1f, 0f));

        episodeText.text = lines[currentIndex];

        // Fade in нового текста
        yield return StartCoroutine(FadeText(0f, 1f));

        isAnimating = false;
    }

    private IEnumerator FadeText(float from, float to)
    {
        float elapsed = 0f;
        Color c = episodeText.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            episodeText.color = c;
            yield return null;
        }
        c.a = to;
        episodeText.color = c;
    }

    private IEnumerator GoToMenu()
    {
        // Fade out текста
        yield return StartCoroutine(FadeText(1f, 0f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(mainMenuScene);
    }
}   