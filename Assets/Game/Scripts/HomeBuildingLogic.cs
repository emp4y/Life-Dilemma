using UnityEngine;
using UnityEngine.UIElements;

public class HomeBuildingLogic : MonoBehaviour
{
    public GameObject MainUI;
    public long MonthCost = 0;
    public int MonthHappiness = 5;
    private void OnEnable()
    {
        LoadUI();
        ReLoadUI();
    }

    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();

        Button close = root.Q<Button>("CloseButton");
        close.clicked += () =>
        {
            gameObject.SetActive(false);
        };

        root.Q<Button>("BuyApartmentButton").clicked += () =>
        {
            mainUiLogic.Home = "Apartment";
            ReLoadUI();
        };

        root.Q<Button>("BuyHouseButton").clicked += () =>
        {
            mainUiLogic.Home = "House";
            ReLoadUI();
        };
    }

    private void ReLoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();

        if (mainUiLogic.Home == "Parent")
        {
            MonthCost = 0;
            MonthHappiness = 5;
        }
        if (mainUiLogic.Home == "Parent")
        {
            MonthCost = 1700000;
            MonthHappiness = 10;
        }
        if (mainUiLogic.Home == "Parent")
        {
            MonthCost = 3000000;
            MonthHappiness = 15;
        }

        root.Q<Label>("IncomeText").text = MonthHappiness.ToString() + "/mo";
        root.Q<Label>("Outcome").text = mainUiLogic.GameCurrencyFormat(MonthCost) + "/mo";
    }
}
