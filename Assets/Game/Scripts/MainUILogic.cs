using UnityEngine;
using UnityEngine.UIElements;

public class MainUILogic : MonoBehaviour
{
    #region Player Values
    public int CurrentHealth = 100, MaxHealth = 100;
    public int Hunger = 100;
    public int Happiness = 100;
    public int Intelligence = 50;
    public long Cash = 0;
    public int Energy = 60;
    public int Age = 10;
    public string Month = "JAN";
    public string Home = "Parent", Job = "none", Insurence = "none", Food = "none", University = "none", Child = "none", Marriage = "none", Retire = "none";
    public string RandomTaskType = "none";
    public int TaskIndex = 0;
    public bool hasBachelor = false, hasMaster = false, hasDoctorate = false;
    #endregion
    public GameObject PopUpUI, OfficeUI, CaffeUI, UniUi, HomeUI;
    public ClosePopUp ClosePopUpComp;

    private void OnEnable()
    {
        LoadUI();
        ReLoadUI();
    }
    public string GameCurrencyFormat(long cashValue)
    {
        string ans = "";
        if (cashValue >= 0 && cashValue < 1000)
        {
            ans = cashValue.ToString();
        }
        if (cashValue >= 1000 && cashValue < 1000000)
        {
            ans = ((float)(cashValue / 1000f)).ToString() + "K";
        }
        if (cashValue >= 1000000 && cashValue < 1000000000)
        {
            ans = ((float)(cashValue / 1000000f)).ToString() + "M";
        }
        if (cashValue >= 1000000000 && cashValue < 1000000000000)
        {
            ans = ((float)(cashValue / 1000000000f)).ToString() + "B";
        }
        return "₮" + ans;
    }
    private void GameNextMonth()
    {
        // DO NOT CODE LIKE THIS I WAS TIRED AF
        if (Month == "DEC")
        {
            Month = "JAN";
            Age++;
        }
        else if (Month == "JAN") Month = "FEB";
        else if (Month == "FEB") Month = "MAR";
        else if (Month == "MAR") Month = "APR";
        else if (Month == "APR") Month = "MAY";
        else if (Month == "MAY") Month = "JUN";
        else if (Month == "JUN") Month = "JUL";
        else if (Month == "JUL") Month = "AUG";
        else if (Month == "AUG") Month = "SEP";
        else if (Month == "SEP") Month = "OCT";
        else if (Month == "OCT") Month = "NOV";
        else if (Month == "NOV") Month = "DEC";
    }
    public void ReLoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label moneyText = root.Q<Label>("MoneyText");
        Label energyText = root.Q<Label>("EnergyText");
        Label happinessText = root.Q<Label>("HappinessText");
        Label hungerText = root.Q<Label>("HungerText");
        Label healthText = root.Q<Label>("HealthText");
        Label ageText = root.Q<Label>("AgeText");
        Label monthText = root.Q<Label>("MonthText");

        moneyText.text = GameCurrencyFormat(Cash);
        energyText.text = Energy.ToString() + "/60";
        happinessText.text = Happiness.ToString() + "/100";
        hungerText.text = Hunger.ToString() + "/100";
        healthText.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();
        ageText.text = "Age: " + Age.ToString();
        monthText.text = Month;
    }
    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button HardSkipButton = root.Q<Button>("HardSkipButton");
        HardSkipButton.clicked += () => { HardSkip(); };
        Button SkipButton = root.Q<Button>("SkipButton");
        SkipButton.clicked += () => { SoftSkip(); };

        Button EnergyAddButton = root.Q<Button>("EnergyAddButton");
        EnergyAddButton.clicked += () => { EnergyFill(); };
    }
    private void CalculateMood(int monthPassed)
    {
        CalculateHappiness(monthPassed);
        CalculateHunger(monthPassed);
        CalculateHealth();
        CalculateUni(monthPassed);
    }
    private void CalculateUni(int monthPassed)
    {
        UniBuildingLogic uniBuildingLogic = UniUi.GetComponent<UniBuildingLogic>();
        long monthCost = 0;
        int duration = 0;
        int extraInt = 0;
        if (uniBuildingLogic.CurrentPlan == "Bachelor" && !hasBachelor) { monthCost = 0; duration = 48; extraInt = 30;}
        if (uniBuildingLogic.CurrentPlan == "Master" && !hasMaster) { monthCost = 300000; duration = 36; extraInt = 25;}
        if (uniBuildingLogic.CurrentPlan == "Doctorate" && !hasDoctorate) { monthCost = 600000; duration = 36; extraInt = 25;}
        for (int i = 0; i < monthPassed; i++)
        {
            if (duration > uniBuildingLogic.YearsPassed)
            {
                if (Cash >= monthCost)
                {
                    Cash -= monthCost;
                    uniBuildingLogic.YearsPassed++;
                }
            }
            else
            {
                Intelligence += extraInt;
                if(uniBuildingLogic.CurrentPlan == "Bachelor") hasBachelor = true;
                if(uniBuildingLogic.CurrentPlan == "Master") hasMaster = true;
                if(uniBuildingLogic.CurrentPlan == "Doctorate") hasDoctorate = true;
                uniBuildingLogic.CurrentPlan = "none";
            }
        }
    }
    private void CalculateIncomeMoney(int monthPassed)
    {
        OfficeBuildingLogic officeBuildingLogic = OfficeUI.GetComponent<OfficeBuildingLogic>();
        if (Age <= 18)
        {
            Cash += (150000 + ((Age - 10) * 20000)) * monthPassed;
        }
        Cash += officeBuildingLogic.WorkCash * monthPassed;
    }
    private void EnergyFill()
    {
        Energy = 60;
        ReLoadUI();
    }
    private void HardSkip()
    {
        if (Energy >= 12)
        {
            Energy -= 12;
            Age++;
            CalculateIncomeMoney(12);
            CalculateMood(12);
            ClosePopUpComp.GetComponent<ClosePopUp>().CallTask(12, Age, Child, Marriage);
        }
        ReLoadUI();
    }
    private void SoftSkip()
    {
        if (Energy >= 1)
        {
            Energy--;
            GameNextMonth();
            CalculateIncomeMoney(1);
            CalculateMood(1);
            ClosePopUpComp.GetComponent<ClosePopUp>().CallTask(1, Age, Child, Marriage);
        }
        ReLoadUI();
    }
    private void CalculateHappiness(int monthPassed)
    {
        HomeBuildingLogic homeBuildingLogic = HomeUI.GetComponent<HomeBuildingLogic>();
        if (Age >= 18 && Age < 25) Happiness -= 5 * monthPassed;
        if (Age >= 25 && Age < 40) Happiness -= 10 * monthPassed;
        if (Age >= 25 && Age < 40) Happiness -= 20 * monthPassed;
        if (Age >= 40 && Age < 150) Happiness -= 30 * monthPassed;

        for (int i = 0; i < monthPassed; i++)
        {
            if (Cash >= homeBuildingLogic.MonthCost)
            {
                Cash -= homeBuildingLogic.MonthCost;
                Happiness += homeBuildingLogic.MonthHappiness;
            }
            else
            {
                Happiness += 5;
            }
        }

        if (Happiness > 100) Happiness = 100;
        if (Happiness < 0) Happiness = 0;
        if (Happiness <= 30) CurrentHealth -= 5;
    }

    private void CalculateHunger(int monthPassed)
    {
        CafeBuildingLogic cafeBuildingLogic = CaffeUI.GetComponent<CafeBuildingLogic>();
        if (Age >= 18 && Age < 25) Hunger -= 5 * monthPassed;
        if (Age >= 25 && Age < 40) Hunger -= 10 * monthPassed;
        if (Age >= 25 && Age < 40) Hunger -= 15 * monthPassed;
        if (Age >= 40 && Age < 150) Hunger -= 20 * monthPassed;

        for (int i = 0; i < monthPassed; i++)
        {
            if (Cash >= cafeBuildingLogic.CostPerMonth)
            {
                Cash -= cafeBuildingLogic.CostPerMonth;
                Hunger += cafeBuildingLogic.PerMonth;
            }
        }

        if (Hunger > 100) Hunger = 100;
        if (Hunger < 0) Hunger = 0;
        if (Hunger <= 30) CurrentHealth -= 5;
    }

    private void CalculateHealth()
    {
        MaxHealth = Age >= 25 ? 100 - (65 - Age) / 2 : 100;
        if (Age >= 65) MaxHealth = 100 - (75 - Age) * 3;

        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        if (CurrentHealth < 0) CurrentHealth = 0;

        if (CurrentHealth == 0)
        {
            Debug.LogWarning("YOU ARE DEAD");
        }
        if (CurrentHealth < 50 && Random.Range(0, MaxHealth) % 3 == 0)
        {
            Debug.LogWarning("YOU ARE DEAD");
        }
    }
}
