using UnityEngine;
using UnityEngine.UIElements;

public class TaskPlannedUILogic : MonoBehaviour
{
    public GameObject MainUI, HoverPlannedUI, PopUpUI;
    private void OnEnable()
    {
        LoadUI();
        ReLoadUI();
    }

    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();

        Button PlannedTaskButton = root.Q<Button>("PlannedTask");
        PlannedTaskButton.clicked += () =>
        {
            HoverPlannedUI.SetActive(true);
            root.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Hidden;
        };

        Button ClaimButon = root.Q<Button>("TaskClaim");
        ClaimButon.clicked += ClaimTaskReward;
    }

    private void ClaimTaskReward()
    {
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();
        ClosePopUp popUpLogic = PopUpUI.GetComponent<ClosePopUp>();

        if (mainUiLogic.RandomTaskType == "short")
        {
            if (mainUiLogic.Cash < popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskCost) return;
            mainUiLogic.Cash -= popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskCost;
            if (popUpLogic.ShortTasks[mainUiLogic.TaskIndex].Type == "Happiness")
            {
                mainUiLogic.Happiness += popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskBenefit;
            }
            if (popUpLogic.ShortTasks[mainUiLogic.TaskIndex].Type == "Hunger")
            {
                mainUiLogic.Hunger += popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskBenefit;
            }
            if (popUpLogic.ShortTasks[mainUiLogic.TaskIndex].Type == "Health")
            {
                mainUiLogic.CurrentHealth += popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskBenefit;
                if (mainUiLogic.CurrentHealth > mainUiLogic.MaxHealth) mainUiLogic.CurrentHealth = mainUiLogic.MaxHealth;
            }
        }
        else if (mainUiLogic.RandomTaskType == "long")
        {
            if (mainUiLogic.Cash < popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskCost) return;
            mainUiLogic.Cash -= popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskCost;
            if (popUpLogic.LongTasks[mainUiLogic.TaskIndex].Type == "Happiness")
            {
                mainUiLogic.Happiness += popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskBenefit;
            }
            if (popUpLogic.LongTasks[mainUiLogic.TaskIndex].Type == "Hunger")
            {
                mainUiLogic.Hunger += popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskBenefit;
            }
            if (popUpLogic.LongTasks[mainUiLogic.TaskIndex].Type == "Health")
            {
                mainUiLogic.CurrentHealth += popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskBenefit;
                if (mainUiLogic.CurrentHealth > mainUiLogic.MaxHealth) mainUiLogic.CurrentHealth = mainUiLogic.MaxHealth;
            }
        }
        mainUiLogic.RandomTaskType = "none";
        mainUiLogic.TaskIndex = 0;
        ReLoadUI();
        mainUiLogic.ReLoadUI();
    }

    public void ReLoadUI()
    {
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();
        ClosePopUp popUpLogic = PopUpUI.GetComponent<ClosePopUp>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        if (mainUiLogic.RandomTaskType == "none")
        {
            root.Q<Label>("TaskTitle").text = "";
            root.Q<Label>("TaskDuration").text = "";
            root.Q<Button>("TaskClaim").style.visibility = Visibility.Hidden;
        }

        if (mainUiLogic.RandomTaskType == "short")
        {
            root.Q<Label>("TaskTitle").text = popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskName;
            root.Q<Label>("TaskDuration").text = popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskDuration.ToString() + "сар.";
            root.Q<Button>("TaskClaim").text = mainUiLogic.GameCurrencyFormat(popUpLogic.ShortTasks[mainUiLogic.TaskIndex].TaskCost);
            root.Q<Button>("TaskClaim").style.visibility = Visibility.Visible;
        }

        if (mainUiLogic.RandomTaskType == "long")
        {
            root.Q<Label>("TaskTitle").text = popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskName;
            root.Q<Label>("TaskDuration").text = popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskDuration.ToString() + "жил.";
            root.Q<Button>("TaskClaim").text = mainUiLogic.GameCurrencyFormat(popUpLogic.LongTasks[mainUiLogic.TaskIndex].TaskCost);
            root.Q<Button>("TaskClaim").style.visibility = Visibility.Visible;
        }
    }
}
