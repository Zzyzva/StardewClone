using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPole : Item
{
    public float castDistance = 2;
    public GameObject bobber;
    public GameObject bobberObject;
    bool cast = false;
    public int energyCost = 15;


    Water.Type waterType;

    //Checks for water, places the bobber, activates fishing
    public override void ItemUse(string facing, Vector3 origin){
        if(!cast){
            Vector3 bobberPos = new Vector3(0,0,0);
            

            //Check direction of cast
            if(facing == "right"){
                    bobberPos = new Vector3(origin.x + castDistance, origin.y, origin.z);
            } else if( facing == "left"){
                    bobberPos = new Vector3(origin.x - castDistance, origin.y, origin.z);
            } else if( facing == "up"){
                    bobberPos = new Vector3(origin.x, origin.y + castDistance, origin.z);
            } else if( facing == "down"){
                    bobberPos = new Vector3(origin.x, origin.y - castDistance, origin.z);
            }

            //Check for water

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(bobberPos, .1f);
            foreach (var hitCollider in hitColliders){
                if(hitCollider.gameObject.GetComponent<Water>()){
                    //Create bobber
                    bobberObject = Instantiate(bobber);
                    bobberObject.transform.position = bobberPos;
                    Player_Manager.CanMove(false);
                    cast = true;
                    waterType = hitCollider.gameObject.GetComponent<Water>().type;
                    Fishing_Manager.instance.fishing = true;
                    break;
                }
            }
        //If already Cast, try to catch   
        } else {
            Fishing_Manager.instance.AttemptCatch(energyCost, waterType);
            cast = false;
            Destroy(bobberObject);
        }
    }     
}
