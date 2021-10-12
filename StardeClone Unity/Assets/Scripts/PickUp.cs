using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Item item;
    private void OnMouseDown() {
    if(!Time_Manager.getInstance().pause){
        Inventory_Manager.instance.AddItem(item, 1);
        Destroy(gameObject);
    }
  }
}
