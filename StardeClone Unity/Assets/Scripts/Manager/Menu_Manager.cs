using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    
    public static Menu_Manager instance;

    bool UIopen = false;

    public CanvasGroup menu;

    public GameObject relationshipUIPrefab;
    public GameObject relationshipMenuContent;


    public List<Script> npcs =  new List<Script>();

    public Sprite emptyHeart;
    public Sprite fullHeart;

    void Awake()
    {
        if(instance){
            Destroy(this);
        } else{
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            if(UIopen){
                menu.alpha = 0f;
                menu.blocksRaycasts = false;
                menu.interactable = false;
                Time_Manager.getInstance().Unpause();
                UIopen = false;
            } else {
                if(!Time_Manager.getInstance().pause){
                    Time_Manager.getInstance().Pause();
                    menu.alpha = 1f;
                    menu.blocksRaycasts = true;
                    menu.interactable = true;
                    UIopen = true;
                    Quest_Manager.instance.OpenMenu();
                    UpdateRealtionshipMenu();
                }
            }     
        }
    }


    public void UpdateRealtionshipMenu(){


        for(int i = 0; i < relationshipMenuContent.transform.childCount; i++){
            Destroy(relationshipMenuContent.transform.GetChild(i).gameObject);
        }
        foreach(Script npc in npcs){
            GameObject UI = Instantiate(relationshipUIPrefab);
            UI.transform.transform.parent = relationshipMenuContent.transform;
            Text text = UI.transform.GetChild(1).GetComponent<Text>();
            
            if(npc.metPlayer == true){
                text.text = npc.npcName;
                UI.transform.GetChild(2).GetComponent<Image>().sprite = npc.icon;
                int hearts = npc.relationship / npc.heartRelationship;
                if(hearts > 4){
                    hearts = 4;
                }
                for(int i = 0; i < 4; i++){
                    UI.transform.GetChild(3).GetChild(i).GetComponent<Image>().sprite = emptyHeart;
                }
                for(int i = 0; i < hearts; i++){
                    UI.transform.GetChild(3).GetChild(i).GetComponent<Image>().sprite = fullHeart;
                }


                
            }else{
                text.text = "???";
            }
        }
    }
}
