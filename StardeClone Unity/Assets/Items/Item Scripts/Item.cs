using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{

    public enum Favor{ hate, dislike, neutral, like, love};

    public Favor favor = Favor.neutral;

    new public string name = "NO NAME";
    public Sprite icon = null;

    public int stackSize = 99;

    [HideInInspector]
    public int count = 0;

    [HideInInspector]
    public Inventory_Slot slot;

    public int value = 0;
    public bool canSell = true;

    public void OnUse(){ 
        //Get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 origin = Player_Manager.player.transform.position;

        Vector3 direction = (mousePos - origin);
        string facing;
        //Check direction of cast
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if(direction.x > 0){
                facing = "right";
            } else{
                facing = "left";
            }
        }else{
            if(direction.y > 0){
                facing = "up";
            } else{
                facing = "down";
            }
        }
        ItemUse(facing, origin);
    }

    public virtual void OnUseUp(){

    } 

    public virtual void ItemUse(string facing, Vector3 origin){

    }

    public virtual bool Update(){
        return true;
    }
}
