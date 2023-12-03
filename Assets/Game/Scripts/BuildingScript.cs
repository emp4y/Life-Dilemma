using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject HoverUI;
    public GameObject HouseHover, BankHover, CafeHover, UniHover, MedicHover, OfficeHover, TaskPopUp, HoverPlanned;

    void OnMouseDown()
    {
        if (!HouseHover.activeSelf && !BankHover.activeSelf && !CafeHover.activeSelf && !UniHover.activeSelf && !MedicHover.activeSelf && !OfficeHover.activeSelf && !TaskPopUp.activeSelf && !HoverPlanned.activeSelf)
        {
            HoverUI.SetActive(true);
        }
    }
}
