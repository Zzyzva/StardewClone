using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButtons : MonoBehaviour
{
    public CanvasGroup giftMenu;
    
    public void ExitPress(){
        Dialogue_Manager.instance.DisplayNextSentence();
    }

    public void QuestPress(){
        Dialogue_Manager.instance.CompleteQuest();
    }

    public void GiftPress(){
        giftMenu.alpha = 1;
        giftMenu.interactable = true;
        giftMenu.blocksRaycasts = true;
    }
}
