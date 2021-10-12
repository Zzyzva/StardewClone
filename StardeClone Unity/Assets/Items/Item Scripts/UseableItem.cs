using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItem : Item
{

    public GameObject swing;
    public float energyCost;
    public float swingTime;
    public string hitMethod;
    bool swinging = false;
    float lastSwing;
    
    GameObject swingInstance;
    


    public override void ItemUse(string facing, Vector3 origin){
        Collider2D[] hitColliders = ItemUseLocation(facing, origin);
        foreach (var hitCollider in hitColliders){
            hitCollider.SendMessage(hitMethod);
        }
    }


    //Same as item use, but does not send hit message
     public Collider2D[] ItemUseLocation(string facing, Vector3 origin){
        if(!swinging){

            lastSwing = Time.time;
            swinging = true;
            Player_Manager.CanMove(false);

            
            if(facing == "right"){
                    origin.x += 1;
            } else if( facing == "left"){
                    origin.x -= 1;
            } else if( facing == "up"){
                    origin.y += 1;
            } else if( facing == "down"){
                    origin.y -= 1;
            }
            Vector3Int position = Vector3Int.RoundToInt(origin);
            swingInstance =  Instantiate(swing);
            swingInstance.transform.position = origin;
            swingInstance.GetComponent<SpriteRenderer>().sprite = icon;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), .1f);
            

            Inventory_Manager.instance.energy -= energyCost;
            return hitColliders;
        }
        Collider2D[] hits = new Collider2D[0];
        return hits;
    }



    public override bool Update() {
        if(swinging){
            if(lastSwing + swingTime <= Time.time){
                swinging = false;
                Player_Manager.CanMove(true);
                Destroy(swingInstance);
                return true;
            }
        }
        return false;
    }
}

