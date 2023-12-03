using UnityEngine;
using UnityEngine.UIElements;

public class MedicBuildingLogic : MonoBehaviour
{
    public GameObject MainUI;
    private void OnEnable()
    {
        LoadUI();
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

        root.Q<Button>("BuyMedicine").clicked += () =>
        {
            if (mainUiLogic.Cash < 200000) return;
            mainUiLogic.Cash -= 200000;
            mainUiLogic.CurrentHealth += 2;
            if(mainUiLogic.CurrentHealth > mainUiLogic.MaxHealth) mainUiLogic.CurrentHealth = mainUiLogic.MaxHealth;
            mainUiLogic.ReLoadUI();
        };

        root.Q<Button>("BuyDoctor").clicked += () =>
        {
            if (mainUiLogic.Cash < 500000) return;
            mainUiLogic.Cash -= 500000;
            mainUiLogic.CurrentHealth += 5;
            if(mainUiLogic.CurrentHealth > mainUiLogic.MaxHealth) mainUiLogic.CurrentHealth = mainUiLogic.MaxHealth;
            mainUiLogic.ReLoadUI();
        };

        root.Q<Button>("BuyHospital").clicked += () =>
        {
            if (mainUiLogic.Cash < 1000000) return;
            mainUiLogic.Cash -= 1000000;
            mainUiLogic.CurrentHealth += 10;
            if(mainUiLogic.CurrentHealth > mainUiLogic.MaxHealth) mainUiLogic.CurrentHealth = mainUiLogic.MaxHealth;
            mainUiLogic.ReLoadUI();
        };
    }
}
