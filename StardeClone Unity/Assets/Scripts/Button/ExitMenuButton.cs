using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuButton : MonoBehaviour
{
    public CanvasGroup menu;
    public void ButtonPress(){
        menu.alpha = 0f;
        menu.interactable = false;
        menu.blocksRaycasts = false;
        Time_Manager.getInstance().Unpause();
    }
}
