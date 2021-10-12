using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShippingMenuButton : MonoBehaviour
{
    public CanvasGroup menu;
    public Inventory_Slot slot;
    public void ButtonPress(){
        menu.alpha = 0f;
        menu.interactable = false;
        menu.blocksRaycasts = false;
        slot.ClearSlot();
        Time_Manager.getInstance().Unpause();
    }
}
