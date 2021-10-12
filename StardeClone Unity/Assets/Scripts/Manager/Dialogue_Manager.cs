using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Dialogue_Manager : MonoBehaviour
{
public Text nameText;
public Text dialogueText;
public CanvasGroup textBox;
public CanvasGroup giftMenu;
public Button questButton;

public static Dialogue_Manager instance;

public Script activeScript;

private Queue<string> sentences;
private bool dialogueActive;
private Quest completeableQuest;

private void Start() {
    if(instance == null){
        instance = this;
        sentences = new Queue<string>();  
    } else{
        Destroy(gameObject);
    }
}

public static Dialogue_Manager getInstance(){
    if(instance == null){
            return new Dialogue_Manager();
        }
        return instance;
}




    public void StartDialogue(Dialogue dialogue, Script script){
        Time_Manager.getInstance().Pause();

        activeScript = script;

        completeableQuest = Quest_Manager.instance.CheckQuests(dialogue.name);
        if(completeableQuest != null){
            questButton.gameObject.SetActive(true);
        }
        
        dialogueActive = true;
        nameText.text = dialogue.name;

        textBox.alpha = 1;
        textBox.interactable = true;
        textBox.blocksRaycasts = true;
        
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    public void CompleteQuest(){
        Quest_Manager.instance.CompleteQuest(completeableQuest);
        questButton.gameObject.SetActive(false);
        
        Dialogue dialogue = activeScript.GetQuestCompleteDialogue(completeableQuest);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void EndDialogue(){
        Time_Manager.getInstance().Unpause();
        dialogueActive = false;

        textBox.alpha = 0;
        textBox.interactable = false;
        textBox.blocksRaycasts = false;

        giftMenu.alpha = 0;
        giftMenu.interactable = false;
        giftMenu.blocksRaycasts = false;

        questButton.gameObject.SetActive(false);
        completeableQuest = null;
        activeScript = null;
    }


    public void AcceptGift(Item item){
        Dialogue dialogue = activeScript.GetGiftDialogue(item);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

        giftMenu.alpha = 0;
        giftMenu.interactable = false;
        giftMenu.blocksRaycasts = false;
    }

}
