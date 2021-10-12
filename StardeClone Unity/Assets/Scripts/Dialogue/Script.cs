using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : Interactable
{

    public string dialgueCSV = "";
    
    protected string introDialogue;
    protected string hateGiftDialogue;
    protected string dislikeGiftDialogue;
    protected string neutralGiftDialogue;
    protected string likeGiftDialogue;
    protected string loveGiftDialogue;
    protected string questDialogue;

    protected string[] zeroHeartsDialogue;
    protected string[] twoHeartsDialogue;
    public string npcName;
    public Sprite icon;


    public bool metPlayer = false;
    public bool talkedToday = false;
    Dialogue currentDialogue;


    public int relationship = 0;

    [HideInInspector]
    public int heartRelationship = 10;



    void Start() {
        //Read dialogue csv and save it
        if(dialgueCSV != ""){
            TextAsset textAsset = Resources.Load<TextAsset>(dialgueCSV);
            string[] data = textAsset.text.Split(new char[] {'\n'});

            introDialogue = data[0].Split(new char[] { '|'})[1];
            hateGiftDialogue = data[1].Split(new char[] { '|'})[1];
            dislikeGiftDialogue = data[2].Split(new char[] { '|'})[1];
            neutralGiftDialogue = data[3].Split(new char[] { '|'})[1];
            likeGiftDialogue = data[4].Split(new char[] { '|'})[1];
            loveGiftDialogue = data[5].Split(new char[] { '|'})[1];
            questDialogue = data[6].Split(new char[] { '|'})[1];

            zeroHeartsDialogue = data[7].Split(new char[] { '|'});
            twoHeartsDialogue = data[8].Split(new char[] { '|'});

        }

        //Add to list of npcs
        Menu_Manager.instance.npcs.Add(this);
        Time_Manager.getInstance().scripts.Add(this);
    }


    public override void Interact() {
        Dialogue_Manager.getInstance().StartDialogue(GetDialogue(), this);
    }

    public Dialogue GetDialogue(){
        if(!talkedToday){
            talkedToday = true;
            relationship++;
            //Create object
            Dialogue ret = new Dialogue();
            ret.name = npcName;

            //Check unique dialogue
            if(!metPlayer){
                metPlayer = true;
                ret.sentences.Add(introDialogue);
                currentDialogue = ret;
                return ret;
            }

            //Standard dialogue
            int size = zeroHeartsDialogue.Length;
            int rand = Random.Range(1,size); //Starts at 1 to skip label
            if(relationship < heartRelationship * 2){
                ret.sentences.Add(zeroHeartsDialogue[rand]);
            } else{
                ret.sentences.Add(twoHeartsDialogue[rand]);
            }
            
            currentDialogue = ret;
            return ret;
        } else{
            return currentDialogue;
        }
    }

    public virtual Dialogue GetQuestCompleteDialogue(Quest quest){
        //Create object
        Dialogue ret = new Dialogue();
        ret.name = npcName;
        ret.sentences.Add(questDialogue);
        return ret;
    }

    public Dialogue GetGiftDialogue(Item item){
        Dialogue ret = new Dialogue();
        ret.name = npcName;

        if(item.favor == Item.Favor.hate){
            relationship -= 5;
            ret.sentences.Add(hateGiftDialogue);
        } else if(item.favor == Item.Favor.dislike){
            relationship -= 1;
            ret.sentences.Add(dislikeGiftDialogue);
        } else if(item.favor == Item.Favor.neutral){
            relationship += 1;
            ret.sentences.Add(neutralGiftDialogue);
        } else if(item.favor == Item.Favor.like){
            relationship += 4;
            ret.sentences.Add(likeGiftDialogue);
        }else if(item.favor == Item.Favor.love){
            relationship += 8;
            ret.sentences.Add(hateGiftDialogue);
        }
        
        return ret;
    }

    public void NewDay(){
        talkedToday = false;
    }
}
