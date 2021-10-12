using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : Interactable
{
    bool picked;
    public Sprite pickedBush;
    public Item berry;
    public override void Interact()
    {  
        if(!picked){
            gameObject.GetComponent<SpriteRenderer>().sprite = pickedBush;
            int count = Random.Range(2,5);
            Inventory_Manager.instance.AddItem(berry, count);
            picked = true;

        }
        
    }

}
