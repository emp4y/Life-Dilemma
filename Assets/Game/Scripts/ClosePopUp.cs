using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClosePopUp : MonoBehaviour
{
    public Texture2D HappinessImage, HungerImage, HealthImage;
    public GameObject MainUI, TaskPlanned;
    #region Tasks
    public class ShortTaskClass
    {
        public string TaskName { get; set; }
        public int TaskDuration { get; set; }
        public string TaskInfo { get; set; }
        public string Type { get; set; }
        public int TaskBenefit { get; set; }
        public long TaskCost { get; set; }
    }
    public class LongTaskClass
    {
        public string TaskName { get; set; }
        public int TaskDuration { get; set; }
        public string TaskInfo { get; set; }
        public string Type { get; set; }
        public int TaskBenefit { get; set; }
        public long TaskCost { get; set; }
    }
    public List<ShortTaskClass> ShortTasks = new List<ShortTaskClass>
    {
        new ShortTaskClass {TaskName = "Эмнэлэгэ орох", TaskDuration = 2, TaskInfo = "Бие нэг л муу байна даа үзүүлдэг ч юм уу?", Type = "Health", TaskBenefit = 3, TaskCost = 200000},
        new ShortTaskClass {TaskName = "Хувцас авах", TaskDuration = 3, TaskInfo = "Converse -д шинэ загвар ирсэн байна лээ очиж авия даа.", Type = "Happiness", TaskBenefit = 2, TaskCost = 300000},
        new ShortTaskClass {TaskName = "Төрсөн өдөр", TaskDuration = 1, TaskInfo = "ӨӨ! Тамирын төрсөн өдөр энэ сар юм байна шүү дээ очдог ч юм билэв үү?", Type = "Happiness", TaskBenefit = 4, TaskCost = 150000},
        new ShortTaskClass {TaskName = "Gym-д явах", TaskDuration = 1, TaskInfo = "Энэ долоо хоногтоо gym-дээ очиё доо.", Type = "Health", TaskBenefit = 4, TaskCost = 70000}
    };
    public List<ShortTaskClass> LongTasks = new List<ShortTaskClass>
    {
        new ShortTaskClass {TaskName = "Байшингаа нэг сайжруулах уу?", TaskDuration = 5, TaskInfo = "Таний дүр байрандаа засвар хийхийг хүсч байна.", Type = "Happiness", TaskBenefit = 15, TaskCost = 50000000},
        new ShortTaskClass {TaskName = "Olymp", TaskDuration = 4, TaskInfo = "Дүр: Би нэрэээ ирэх olymp ийг үздэг ч юм уу гэж төлөвлөж байгаашд, хамт явах уу?", Type = "Happiness", TaskBenefit = 10, TaskCost = 15000000},
        new ShortTaskClass {TaskName = "Бүтэн үзлээ", TaskDuration = 1, TaskInfo = "Гэр бүлээрээ ирэх жилд бүтэн үзлэгт ориё доо.", Type = "Health", TaskBenefit = 8, TaskCost = 5000000},
        new ShortTaskClass {TaskName = "Машин авах уу?", TaskDuration = 3, TaskInfo = "Түгжээ багасцан чинь машин авмаар ч юм шиг.", Type = "Happiness", TaskBenefit = 8, TaskCost = 70000000}
    };
    #endregion
    public void CallTask(int monthPassed, int Age, string Child, string Marriage)
    {
        VisualElement taskPlannedUi = TaskPlanned.GetComponent<UIDocument>().rootVisualElement;   
        // Planned Tasks
        #region Planned Tasks
        if (Age == 18)
        {
            taskPlannedUi.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Visible;
            Debug.Log("Universityd orh task");
            //University task to task holder
        }
        if (Age >= 20)
        {
            taskPlannedUi.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Visible;
            Debug.Log("Ajild orh task");
        }
        if (Age >= 25)
        {
            taskPlannedUi.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Visible;
            Debug.Log("Gerlelt (optional) // 40 hurhed gerelsen baih ystoi");
        }
        if (Age >= 30)
        {
            taskPlannedUi.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Visible;
            Debug.Log("Child (optional) // 40 hurhed biylsen bh ystoi");
        }
        if (Age == 40 && Child == "none" && Marriage == "none")
        {
            taskPlannedUi.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Visible;
            Debug.Log("Max Happiness = 50");
        }
        if (Age == 65)
        {
            taskPlannedUi.Q<VisualElement>("PlannedNotif").style.visibility = Visibility.Visible;
            Debug.Log("Retired, can side hustle");
        }
        #endregion
        for (int i = 0; i < monthPassed && Age >= 20 && !gameObject.activeSelf; i++)
        {
            // Generate Random task
            MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();
            // Mental health
            if (mainUiLogic.RandomTaskType == "none")
            {
                bool isLongTask = Random.Range(1, 60) == 5;
                if (isLongTask)
                {
                    int randomTaskIndex = Random.Range(0, LongTasks.Count - 1);
                    Debug.Log("Long Task : " + LongTasks[randomTaskIndex].TaskName);
                    //CallLongPopUp
                    mainUiLogic.RandomTaskType = "long";
                    mainUiLogic.TaskIndex = randomTaskIndex;
                }
                else
                {
                    int randomTaskIndex = Random.Range(0, LongTasks.Count - 1);
                    Debug.Log("Short Task : " + ShortTasks[randomTaskIndex].TaskName);
                    CallShortPopUp(ShortTasks[randomTaskIndex]);
                    mainUiLogic.RandomTaskType = "short";
                    mainUiLogic.TaskIndex = randomTaskIndex;
                }
            }
            // Main
        }
    }

    private void CallShortPopUp(ShortTaskClass currentTask)
    {
        gameObject.SetActive(true);
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label Title = root.Q<Label>("TaskTitle");
        Label Description = root.Q<Label>("TaskDescription");
        VisualElement TypeIcon = root.Q<VisualElement>("RewardType");
        Label Reward = root.Q<Label>("TaskReward");
        Label Duration = root.Q<Label>("TaskDuration");

        Title.text = currentTask.TaskName;
        Description.text = currentTask.TaskInfo;
        Duration.text = "Хугацаа : " + currentTask.TaskDuration.ToString() + " сар";
        Reward.text = currentTask.TaskBenefit.ToString();
        if (currentTask.Type == "Happiness")
        {
            TypeIcon.style.backgroundImage = new StyleBackground(HappinessImage);
        }
        if (currentTask.Type == "Hunger")
        {
            TypeIcon.style.backgroundImage = new StyleBackground(HungerImage);
        }
        if (currentTask.Type == "Health")
        {
            TypeIcon.style.backgroundImage = new StyleBackground(HealthImage);
        }
    }

    private void OnEnable()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        TaskPlannedUILogic taskPlannedUILogic = TaskPlanned.GetComponent<TaskPlannedUILogic>();

        Button close = root.Q<Button>("Close");
        close.clicked += () =>
        {
            gameObject.SetActive(false);
            taskPlannedUILogic.ReLoadUI();
        };
    }
}
