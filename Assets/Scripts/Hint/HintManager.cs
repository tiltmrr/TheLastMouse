using UnityEngine;

public class HintManager : MonoBehaviour
{
    public Hint hint;
    public bool isPuzzleMode = false;

    private string[] storyHints = {
        "Осмотрись вокруг... Может найдёшь что-то чтобы открыть дверь?",
        "Нужно выбраться отсюда. Поищи выход."
    };

    private string[] puzzleHints = {
        "Для двоичных чисел загляни в руководство!",
        "Подсказка: 1402 — дата рождения Эдгара."
    };

    private int currentHintIndex = 0;

    public void ShowCurrentHint()
    {
        if (isPuzzleMode)
        {
            hint.DefineHintType(true);
            hint.ShowHint(puzzleHints[Random.Range(0, puzzleHints.Length)]);
        }
        else
        {
            hint.DefineHintType(false);
            if (currentHintIndex < storyHints.Length)
                hint.ShowHint(storyHints[currentHintIndex]);
        }
    }

    public void NextHint()
    {
        if (currentHintIndex < storyHints.Length - 1)
            currentHintIndex++;
    }
}