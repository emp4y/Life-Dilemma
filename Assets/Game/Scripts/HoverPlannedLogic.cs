using UnityEngine;
using UnityEngine.UIElements;

public class HoverPlannedLogic : MonoBehaviour
{
    public GameObject MainUI;
    private void OnEnable()
    {
        LoadUI();
        ReLoadUI();
    }

    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button close = root.Q<Button>("Close");
        close.clicked += () =>
        {
            gameObject.SetActive(false);
        };
    }

    private void ReLoadUI()
    {
        MainUILogic mainUiLogic = MainUI.GetComponent<MainUILogic>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        if(mainUiLogic.Age >= 18 && mainUiLogic.University != "none")
        {
            root.Q<VisualElement>("18Check").style.visibility = Visibility.Visible;
        }else root.Q<VisualElement>("18Check").style.visibility = Visibility.Hidden;

        if (mainUiLogic.Age >= 20 && mainUiLogic.Job != "none")
        {
            root.Q<VisualElement>("22Check").style.visibility = Visibility.Visible;
        }
        else root.Q<VisualElement>("22Check").style.visibility = Visibility.Hidden;

        if (mainUiLogic.Age >= 25 && mainUiLogic.Marriage != "none")
        {
            root.Q<VisualElement>("25Check").style.visibility = Visibility.Visible;

        }
        else root.Q<VisualElement>("25Check").style.visibility = Visibility.Hidden;

        if (mainUiLogic.Age >= 30 && mainUiLogic.Child != "none")
        {
            root.Q<VisualElement>("30Check").style.visibility = Visibility.Visible;
        }
        else root.Q<VisualElement>("30Check").style.visibility = Visibility.Hidden;

        if (mainUiLogic.Age >= 40 && mainUiLogic.Marriage != "none")
        {
            root.Q<VisualElement>("40Check").style.visibility = Visibility.Visible;
        }
        else root.Q<VisualElement>("40Check").style.visibility = Visibility.Hidden;
        if (mainUiLogic.Age >= 41 && mainUiLogic.Marriage != "none")
        {
            root.Q<VisualElement>("41Check").style.visibility = Visibility.Visible;
        }

        else root.Q<VisualElement>("41Check").style.visibility = Visibility.Hidden;
        if (mainUiLogic.Age >= 65 && mainUiLogic.Retire != "none")
        {
            root.Q<VisualElement>("65Check").style.visibility = Visibility.Visible;
        }
        else root.Q<VisualElement>("65Check").style.visibility = Visibility.Hidden;
    }
}
