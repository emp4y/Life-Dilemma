
using UnityEngine;
using UnityEngine.UIElements;

public class UniBuildingLogic : MonoBehaviour
{
    public GameObject MainUI;
    public string CurrentPlan = "none";
    public int YearsPassed = 0;
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

        root.Q<Button>("LearnBachelor").clicked += () =>
        {
            if(mainUiLogic.Age < 18) return;
            CurrentPlan = "Bachelor";
            YearsPassed = 1;
            ReLoadUI();
        };
        root.Q<Button>("LearnMaster").clicked += () =>
        {
            if(mainUiLogic.Age < 18) return;
            CurrentPlan = "Master";
            YearsPassed = 1;
            ReLoadUI();
        };
        root.Q<Button>("LearnDoctorate").clicked += () =>
        {
            if(mainUiLogic.Age < 18) return;
            CurrentPlan = "Doctorate";
            YearsPassed = 1;
            ReLoadUI();
        };
    }

    private void ReLoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();

        root.Q<Label>("IncomeText").text = "Intelligence : " + mainUiLogic.Intelligence.ToString();
        root.Q<Label>("CurrentPlanText").text = "Current : " + CurrentPlan + (YearsPassed == 0 ? "" : " (" + YearsPassed + ")");
    }
}
