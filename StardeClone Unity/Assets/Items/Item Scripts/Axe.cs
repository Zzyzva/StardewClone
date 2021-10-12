using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Item
{

    public GameObject axeSwing;
    public float energyCost = 3;
    public bool swinging = false;
    public float lastSwing;
    public float swingTime = .75f;
    public GameObject axeSwinginstance;
    


    public override void ItemUse(string facing, Vector3 origin){
        if(!swinging){
            lastSwing = Time.time;
            swinging = true;
            Player_Manager.CanMove(false);
            if(facing == "right"){
                    axeSwinginstance =  Instantiate(axeSwing);
                    origin.x += 1;
                    axeSwinginstance.transform.position = origin;
            } else if( facing == "left"){
                    axeSwinginstance =  Instantiate(axeSwing);
                    origin.x -= 1;
                    axeSwinginstance.transform.position = origin;
            } else if( facing == "up"){
                    axeSwinginstance =  Instantiate(axeSwing);
                    origin.y += 1;
                    axeSwinginstance.transform.position = origin;
            } else if( facing == "down"){
                    axeSwinginstance =  Instantiate(axeSwing);
                    origin.y -= 1;
                    axeSwinginstance.transform.position = origin;
            }


            Inventory_Manager.instance.energy -= energyCost;
        }
    }

    public override bool Update() {
        if(swinging){
            if(lastSwing + swingTime <= Time.time){
                swinging = false;
                Player_Manager.CanMove(true);
                Destroy(axeSwinginstance);
                return true;
            }
        }
        return false;
    }


    
}
