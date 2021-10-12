using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string itemName;
    public int quantity;
    public string npc;
    public bool active;

    public Quest(string itemName, int quantity, string npc, bool active){
        this.itemName = itemName;
        this.quantity = quantity;
        this.npc = npc;
        this.active = active;
    }
}
