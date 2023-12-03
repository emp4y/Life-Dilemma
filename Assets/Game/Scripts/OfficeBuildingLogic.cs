using UnityEngine;
using UnityEngine.UIElements;

public class OfficeBuildingLogic : MonoBehaviour
{
    public GameObject MainUI;
    public long WorkCash = 0;
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

        root.Q<Button>("WorkButton").clicked += () =>
        {
            if(mainUiLogic.Age < 20) return;
            WorkCash = 600000 + ((mainUiLogic.Intelligence -50) * 80000);
            ReLoadUI();
        };
    }

    private void ReLoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();

        root.Q<Label>("Extra").text = "Extra(Intelligence) : " + ((mainUiLogic.Intelligence - 50) * 80000).ToString();
        root.Q<Label>("IncomeText").text = mainUiLogic.GameCurrencyFormat(WorkCash) + "/mo";
        root.Q<Button>("WorkButton").style.backgroundColor = (mainUiLogic.Age < 20 ) ? new Color(111f/255f,111f/255f,111f/255f,1f) : new Color(81f/255f,237/255f,96f/255f,1f);
    }
}
