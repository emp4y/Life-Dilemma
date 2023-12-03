using UnityEngine;
using UnityEngine.UIElements;

public class CafeBuildingLogic : MonoBehaviour
{
    public int PerMonth = 0;
    public long CostPerMonth = 0;
    public GameObject MainUI;
    private void OnEnable()
    {
        LoadUI();
        ReLoadUI();
    }

    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button close = root.Q<Button>("CloseButton");
        close.clicked += () =>
        {
            gameObject.SetActive(false);
        };
        Button SimplePlan = root.Q<Button>("EquipSimplePlan");
        SimplePlan.clicked += () => {
            CostPerMonth = 150000;
            PerMonth = 5;
            ReLoadUI();
        };
        Button ComplexPlan = root.Q<Button>("EquipComplexPlan");
        ComplexPlan.clicked += () => {
            CostPerMonth = 300000;
            PerMonth = 10;
            ReLoadUI();
        };
        Button LuxuryPlan = root.Q<Button>("EquipLuxuryPlan");
        LuxuryPlan.clicked += () => {
            CostPerMonth = 500000;
            PerMonth = 15;
            ReLoadUI();
        };
    }

    private void ReLoadUI()
    {
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Label>("IncomeText").text = PerMonth.ToString() + "/mo";
        root.Q<Label>("OutcomeText").text = mainUiLogic.GameCurrencyFormat(CostPerMonth) + "/mo";
    }
}
