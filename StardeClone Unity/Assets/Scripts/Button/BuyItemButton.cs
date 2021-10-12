using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemButton : MonoBehaviour
{
    public VendorInventory.VendorItem item;


    public void ButtonPress(){
        if(Inventory_Manager.instance.gold >= item.price){
            Inventory_Manager.instance.AddItem(item.item, 1);
            Inventory_Manager.instance.gold -= item.price;
        }
    }
}
