using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptQuestButton : MonoBehaviour
{
    public Quest quest;
    public void ButtonPress(){
        quest.active = true;
        Quest_Manager.instance.loadBulletinQuests();
    }
}
