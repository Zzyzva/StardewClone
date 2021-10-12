using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int totalChops = 3;
    public int chops = 0;
    public Item wood;


    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "AxeSwing(Clone)"){
            chops++;
        }
        if(chops >= totalChops){
            int count = Random.Range(10,15);
            Inventory_Manager.instance.AddItem(wood, count);
            Destroy(gameObject);
            
        }
    }
}
