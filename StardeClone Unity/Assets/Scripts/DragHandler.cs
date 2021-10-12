using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    //Visually moves the item to follow cursor
   public void OnDrag(PointerEventData eventData){
       transform.GetChild(0).position = Input.mousePosition;
   }

    //Returns object to start if not dropped on something
   public void OnEndDrag(PointerEventData eventData){
       transform.GetChild(0).localPosition = Vector3.zero;
   }

    //If dropped on another slot, places it there
    //Swaps with object in that slot
   public void OnDrop(PointerEventData eventData){
        Inventory_Slot newSlot = gameObject.GetComponent<Inventory_Slot>();
        Inventory_Slot oldSlot = eventData.pointerDrag.GetComponent<Inventory_Slot>();

        Item dropItem = oldSlot.item;
        Item replaceItem = newSlot.item;
        //If we are actually dragging something
        if(dropItem){
            //If we are dragging an item out of shipping, sell the replaced item, return dragged item to inventory
            if(oldSlot.slotType == Inventory_Slot.SlotType.shipping){
                Inventory_Manager.instance.inventory.Add(dropItem);
                if(replaceItem){
                    Inventory_Manager.instance.SellItem(replaceItem);
                }
                Inventory_Manager.instance.toSell.Remove(dropItem); 
            }
            //If we are dragging an item into shipping, sell it and clear the old slot
            if(newSlot.slotType == Inventory_Slot.SlotType.shipping){
                if(dropItem.canSell){
                    SetSlot(dropItem, newSlot);
                    oldSlot.ClearSlot();
                    Inventory_Manager.instance.SellItem(dropItem);
                }
                return;
            }
            
            //If we are dragging an item into gifting
            if(newSlot.slotType == Inventory_Slot.SlotType.gift){
                if(dropItem.canSell){
                    Inventory_Manager.instance.RemoveItem(dropItem, 1);
                    Dialogue_Manager.instance.AcceptGift(dropItem);
                }
                return;
            }

        SwitchSlots(oldSlot, newSlot);
        }
        
        
   }

    //Visually switches the items in two slots
    public void SwitchSlots(Inventory_Slot oldSlot, Inventory_Slot newSlot){
        Item dropItem = oldSlot.item;
        Item replaceItem = newSlot.item;


        oldSlot.ClearSlot();
        newSlot.ClearSlot();
        if(dropItem){
            newSlot.SetItem(dropItem);
            dropItem.slot = newSlot;
        }
        if(replaceItem){
            oldSlot.SetItem(replaceItem);
            replaceItem.slot = oldSlot;
        
        }
    }


    public void SetSlot(Item item, Inventory_Slot slot){
        slot.ClearSlot();
        item.slot.item = null;
        item.slot = slot;
        slot.SetItem(item);
    }
}
