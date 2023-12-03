using UnityEngine;
using UnityEngine.UIElements;

public class BankBuildingLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button close = root.Q<Button>("CloseButton");
        close.clicked += () =>
        {
            gameObject.SetActive(false);
        };
    }
}
