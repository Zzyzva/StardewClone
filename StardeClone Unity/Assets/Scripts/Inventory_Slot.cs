using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
    public Image highLight;
    public Image icon;
    public Item item;
    public Text countText;
    
    public enum SlotType {inventory, shipping, gift};

    public SlotType slotType;
    
    private void Start() {
        icon.enabled = false;
    }

    public void Select(){
        highLight.enabled = true;
    }

    public void Unselect(){
        highLight.enabled = false;
    }

    public void SetItem(Item newItem){
        item = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        if(item.count == 1 || item.count == 0){
            countText.text = "";
        } else{
            countText.text = item.count.ToString();
        }
        
    }

    //Removes an item from the slot visually
    public void ClearSlot(){
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }
}
