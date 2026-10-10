using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Puzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    public TextMeshProUGUI[] rowResults; // 4 текста
    public Button[] bitButtons; // 16 кнопок, по рядам: 0-3, 4-7, 8-11, 12-15
    public bool isCompleted = false;

    // Код 1402:
    // 0001 = 1
    // 0100 = 4  
    // 0000 = 0
    // 0010 = 2
    private int[] targetCode = { 1, 4, 0, 2 };
    private int[,] bits = new int[4, 4];
    private bool isActive = false;

    void Start()
    {
        puzzlePanel.SetActive(false);
        for (int i = 0; i < 16; i++)
        {
            int index = i;
            bitButtons[i].onClick.AddListener(() => ToggleBit(index));
        }
        ResetBits();
    }

    public void StartPuzzle()
    {
        isActive = true;
        puzzlePanel.SetActive(true);
        // переключить режим подсказки
        HintManager hintManager = FindFirstObjectByType<HintManager>();
        if (hintManager != null) hintManager.isPuzzleMode = true;
        ResetBits();
    }

    public void ExitPuzzle()
    {
        isActive = false;
        puzzlePanel.SetActive(false);
        HintManager hintManager = FindFirstObjectByType<HintManager>();
        if (hintManager != null) hintManager.isPuzzleMode = false;
        ResetBits();
    }

    public void RestartPuzzle()
    {
        ResetBits();
    }

    public void ToggleBit(int index)
    {
        int row = index / 4;
        int col = index % 4;
        bits[row, col] = bits[row, col] == 0 ? 1 : 0;
        bitButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = bits[row, col].ToString();
        CheckAnswer();
    }

    private void ResetBits()
    {
        bits = new int[4, 4];
        for (int i = 0; i < bitButtons.Length; i++)
        {
            bitButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "0";
        }
        // сброс результатов
        if (rowResults != null)
        {
            foreach (var r in rowResults)
            {
                if (r != null) r.text = "0";
            }
        }
    }

    private void CheckAnswer()
    {
        int[] results = new int[4];
        bool correct = true;

        for (int row = 0; row < 4; row++)
        {
            int value = 0;
            for (int col = 0; col < 4; col++)
            {
                value += bits[row, col] * (int)Mathf.Pow(2, 3 - col);
            }
            results[row] = value;
            if (rowResults != null && row < rowResults.Length)
                rowResults[row].text = value.ToString();
            if (results[row] != targetCode[row]) correct = false;
        }

        if (correct)
        {
            if (rowResults != null && rowResults.Length > 0)
                rowResults[0].text = "Верно!";
            isCompleted = true;
            OnCompleted();
        }
    }

    public void OnCompleted()
    {
        SafeInteract safe = FindFirstObjectByType<SafeInteract>();
        if (safe != null) safe.OpenSafe();
        puzzlePanel.SetActive(false); // закрыть панель
    }
}