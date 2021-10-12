using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTabButton : MonoBehaviour
{
    public CanvasGroup questMenu;
    public CanvasGroup relationshipMenu;
    public void ButtonPressQuests(){
        ClearAll();
        questMenu.alpha = 1f;
        questMenu.interactable = true;
        questMenu.blocksRaycasts = true;
    }

    public void ButtonPressRelationship(){
        ClearAll();
        relationshipMenu.alpha = 1f;
        relationshipMenu.interactable = true;
        relationshipMenu.blocksRaycasts = true;
    }


    void ClearAll(){
        questMenu.alpha = 0f;
        questMenu.interactable = false;
        questMenu.blocksRaycasts = false;

        relationshipMenu.alpha = 0f;
        relationshipMenu.interactable = false;
        relationshipMenu.blocksRaycasts = false;
    }
}
