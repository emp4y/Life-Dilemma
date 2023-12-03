using UnityEngine;
using UnityEngine.UIElements;

public class MainHoverController : MonoBehaviour
{
    public int age, cash, energy, season, hunger, happiness, health, primary, university;
    public GameObject myObject;
    private void OnEnable()
    {
        season = PlayerPrefs.GetInt("Season");
        age = PlayerPrefs.HasKey("Age") ? PlayerPrefs.GetInt("Age") : 10;
        cash = PlayerPrefs.GetInt("Cash");
        energy = PlayerPrefs.HasKey("Energy") ? PlayerPrefs.GetInt("Energy") : 20;
        hunger = PlayerPrefs.HasKey("Hunger") ? PlayerPrefs.GetInt("Hunger") : 100;
        health = PlayerPrefs.HasKey("Health") ? PlayerPrefs.GetInt("Health") : 100;
        happiness = PlayerPrefs.HasKey("Happiness") ? PlayerPrefs.GetInt("Happiness") : 100;
        primary = PlayerPrefs.GetInt("Primary");
        university = PlayerPrefs.GetInt("University");
        UIActions();
        LoadUISettings();
    }
    private void LoadUISettings()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label CashLabel = root.Q<Label>("CashLabel");
        Label EnergyLabel = root.Q<Label>("EnergyLabel");
        Label AgeLabel = root.Q<Label>("AgeLabel");

        CashLabel.text = cash.ToString() + "$";
        EnergyLabel.text = energy.ToString() + "/20";
        AgeLabel.text = age.ToString();

        root.Q<VisualElement>("Winter").style.backgroundColor = (season >= 0) ? new Color(182f/255f, 233f/255f, 255f/255f, 1f) : new Color(0f, 0f, 0f, 150f/255f);
        root.Q<VisualElement>("Spring").style.backgroundColor = (season >= 1) ? new Color(167f/255f, 255f/255f, 109f/255f, 1f) : new Color(0f, 0f, 0f, 150f/255f);
        root.Q<VisualElement>("Summer").style.backgroundColor = (season >= 2) ? new Color(255f/255f, 134f/255f, 139f/255f, 1f) : new Color(0f, 0f, 0f, 150f/255f);
        root.Q<VisualElement>("Autumn").style.backgroundColor = (season >= 3) ? new Color(255f/255f, 240f/255f, 90f/255f, 1f) : new Color(0f, 0f, 0f, 150f/255f);
    }
    private void UIActions()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button HardSkipButton = root.Q<Button>("HardSkipButton");
        HardSkipButton.clicked += HardSkip;
        Button SoftSkipButton = root.Q<Button>("SoftSkipButton");
        SoftSkipButton.clicked += SoftSkip;
        Button AgeButton = root.Q<Button>("AgeButton");
        AgeButton.clicked += ShowStatus;
    }
    private void HardSkip()
    {
        if (energy >= 4)
        {
            age++;
            energy -= 4;
        }
        LoadUISettings();
    }
    private void SoftSkip()
    {
        if (energy >= 1)
        {
            if (season == 3)
            {
                season = 0;
                age++;
            }
            else
            {
                season++;
            }
            energy--;
        }
        LoadUISettings();
    }
    private void ShowStatus()
    {
        myObject.SetActive(true);
    }
}
