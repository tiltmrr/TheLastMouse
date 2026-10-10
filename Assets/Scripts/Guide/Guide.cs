using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guide : MonoBehaviour
{
    public GameObject guidePanel;
    public GameObject overlay; 
    public TextMeshProUGUI leftPageText;
    public TextMeshProUGUI rightPageText;
    public Button prevButton;
    public Button nextButton;

    private int currentPage = 0;

    //  ажда€ пара Ч лева€ и права€ страница
    private string[,] pages = {
    {
        "¬ведение\n\n“ы играешь за Ёдгара Ч молодого мышонка-изобретател€. “вои родители-учЄные —карлет и ’аррисон были уведены злыми котами.",
        "“во€ цель Ч найти родителей и спасти их. »сследуй локации, собирай предметы и решай головоломки!"
    },
    {
        "”правление\n\nЋева€ кнопка мыши Ч идти к указанной точке.\n\nѕрава€ кнопка мыши Ч крастьс€. Ёдгар движетс€ тише и медленнее.",
        " ликай на предметы и персонажей чтобы взаимодействовать с ними.\n\n нопка подсказки поможет если не знаешь что делать дальше."
    },
    {
        "»нвентарь\n\n ликни на предмет на сцене Ч Ёдгар его осмотрит и подберЄт.\n\nЌажми кнопку сумки внизу справа чтобы открыть инвентарь.",
        "ѕредметы в инвентаре сохран€ютс€ при переходе между локаци€ми.\n\nЌекоторые предметы можно использовать дл€ открыти€ замков и решени€ задач."
    },
    {
        "ƒвоична€ система\n\n0 = 0000\n1 = 0001\n2 = 0010\n3 = 0011\n4 = 0100\n5 = 0101",
        "6 = 0110\n7 = 0111\n8 = 1000\n9 = 1001\n\n аждый бит Ч это 0 или 1.\n4 бита = одно число от 0 до 15."
    },
    {
        "ѕодсказка дл€ сейфа\n\n од сейфа Ч особа€ дата.\nѕодумай что важно дл€ семьи Ёдгара...",
        "1402 Ч дата рождени€ Ёдгара.\n\n0001 = 1\n0100 = 4\n0000 = 0\n0010 = 2\n\n”дачи!"
    }
};

    void Start()
    {
        guidePanel.SetActive(false);
        prevButton.onClick.AddListener(PrevPage);
        nextButton.onClick.AddListener(NextPage);
        ShowPage(0);
    }

    public void Open()
    {
        overlay.SetActive(true);
        guidePanel.SetActive(true);
        ShowPage(0);
    }

    public void Close()
    {
        overlay.SetActive(false);
        guidePanel.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < pages.GetLength(0) - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    public void ShowPage(int pageIndex)
    {
        currentPage = pageIndex;
        leftPageText.text = pages[currentPage, 0];
        rightPageText.text = pages[currentPage, 1];
        prevButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive(currentPage < pages.GetLength(0) - 1);
    }
}