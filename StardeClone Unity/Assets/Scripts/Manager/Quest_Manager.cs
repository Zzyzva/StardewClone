using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Manager : MonoBehaviour
{


    public static Quest_Manager instance;
    public List<Quest> quests;
    bool UIopen = false;
    public CanvasGroup questMenu;
    public GameObject questUIPrefab;
    public GameObject questMenuContent;

    public CanvasGroup bulletinMenu;
    public GameObject bulletinUIPrefab;
    public GameObject bulletinMenuContent;

    
    void Awake()
    {
       if(instance){
           Destroy(this);
       }else{
           instance = this;
       }
    }

    void Start(){
        quests = new List<Quest>();
        quests.Add(new Quest("Rose", 1, "Sophia", false));
        
        
    }




    public void OpenMenu(){
        for(int i = 0; i < questMenuContent.transform.childCount; i++){
            Destroy(questMenuContent.transform.GetChild(i).gameObject);
        }
        foreach(Quest quest in quests){
            if(quest.active == true){
                GameObject UI = Instantiate(questUIPrefab);
                UI.transform.transform.parent = questMenuContent.transform;
                Text text = UI.transform.GetChild(1).GetComponent<Text>();
                text.text = "Bring " + quest.quantity + " " + quest.itemName + " to " + quest.npc;
            } 
        }
    }

    //If an npc has two completeable quests it will only show one
    public Quest CheckQuests(string npc){
        foreach(Quest quest in quests){
            if(quest.npc == npc && quest.active){
                int total = 0;
                List<Item> inv = Inventory_Manager.instance.inventory;
                foreach(Item item in inv){
                    if(item.name == quest.itemName){
                        total++;
                        if(total >= quest.quantity){
                            return quest;
                        }
                    }
                }
                
            }
        }
        return null;
    }


    //If the quest can't actually be completed this is buggy
    public void CompleteQuest(Quest quest){
        List<Item> inv = Inventory_Manager.instance.inventory;
        Inventory_Manager.instance.RemoveItemByName(quest.itemName, quest.quantity);
        quests.Remove(quest);
    }   

    public void loadBulletinQuests(){
        //Destorys old quests on the board
        for(int i = 0; i < bulletinMenuContent.transform.childCount; i++){
            Destroy(bulletinMenuContent.transform.GetChild(i).gameObject);
        }
        //Loads new quests
        foreach(Quest quest in quests){
            if(quest.active == false){
                GameObject UI = Instantiate(bulletinUIPrefab);
                UI.transform.transform.parent = bulletinMenuContent.transform;
                UI.transform.GetChild(2).GetComponent<AcceptQuestButton>().quest = quest;
                Text text = UI.transform.GetChild(1).GetComponent<Text>();
                text.text = "Bring " + quest.quantity + " " + quest.itemName + " to " + quest.npc;
            } 
        }
    }
}
