using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum HintType
{
    Story,
    Puzzle
}

public class Hint : MonoBehaviour
{
    public GameObject hintPanel;
    public TextMeshProUGUI hintText;
    public float displayTime = 4f;
    public float fadeTime = 1f;

    public HintType hintType;
    private Image panelImage;
    private Coroutine currentHint;

    void Start()
    {
        panelImage = hintPanel.GetComponent<Image>();
        hintPanel.SetActive(false);
    }

    public void DefineHintType(bool puzzleIsActive)
    {
        hintType = puzzleIsActive ? HintType.Puzzle : HintType.Story;
    }

    public void ShowHint(string text)
    {
        if (currentHint != null)
            StopCoroutine(currentHint);
        currentHint = StartCoroutine(ShowAndFade(text));
    }

    public void HideHint()
    {
        if (currentHint != null)
            StopCoroutine(currentHint);
        hintPanel.SetActive(false);
    }

    private IEnumerator ShowAndFade(string text)
    {
        // показать
        hintPanel.SetActive(true);
        hintText.text = text;

        // полная непрозрачность
        SetAlpha(1f);

        // подождать
        yield return new WaitForSeconds(displayTime);

        // плавно исчезнуть
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            SetAlpha(1f - elapsed / fadeTime);
            yield return null;
        }

        hintPanel.SetActive(false);
    }

    private void SetAlpha(float alpha)
    {
        if (panelImage != null)
        {
            Color c = panelImage.color;
            panelImage.color = new Color(c.r, c.g, c.b, alpha);
        }
        if (hintText != null)
        {
            Color c = hintText.color;
            hintText.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}