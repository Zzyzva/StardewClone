using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item
{

    public GameObject bowCharge;
    GameObject bowChargeInstance;
    public GameObject arrow;
    public float speed = 10f;


    float charge = 0;
    bool charging = false;

    //This is terrible way to do this
    float barTop = 6.18f;

    public override void OnUseUp(){
        charging = false;
        Inventory_Manager.instance.canChangeSelected = true;
        Destroy(bowChargeInstance);


        float angleAdjust;
        float tempSpeed;
        float maxDistance;


        //Red
        if(charge <= 60 || charge > 90){
            angleAdjust = 35;
            tempSpeed = speed / 3;
            maxDistance = 4;
        
        //Yellow
        } else if( charge <= 80){
            angleAdjust = 20;
            tempSpeed = speed / 1.5f;
            maxDistance = 8;

        //Green
        } else {
            angleAdjust = 1;
            tempSpeed = speed;
            maxDistance = 16;
        }
        angleAdjust = Random.Range(-angleAdjust, angleAdjust);




        //Instantiate arrow
        Vector3 origin = Player_Manager.player.transform.position;
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = Player_Manager.player.transform.position.z;
        
        Vector3 shootDirection = mousePos - origin;
        shootDirection = Quaternion.AngleAxis(angleAdjust, new Vector3(0,0,1)) * shootDirection;

        shootDirection = Vector3.Normalize(shootDirection);
        
        
        float angle = AngleBetweenTwoPoints(origin, shootDirection);
        GameObject arrowInstance = Instantiate(arrow, origin, Quaternion.Euler(new Vector3(0,0,angle + 90)));

        Vector2 v = shootDirection * tempSpeed;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90;
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        arrowInstance.GetComponent<Rigidbody2D>().velocity = shootDirection * tempSpeed;
        arrowInstance.GetComponent<Arrow>().maxDistanxce = maxDistance;
        arrowInstance.GetComponent<Arrow>().origin = origin;
        arrowInstance.tag = "Weapon";


        Player_Manager.CanMove(true);
        

     }
 
     float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }

    

    public override void ItemUse(string facing, Vector3 origin){
        Player_Manager.CanMove(false);
        charging = true;
        charge = 0;
        Inventory_Manager.instance.canChangeSelected = false;
        bowChargeInstance = Instantiate(bowCharge);
        origin.x += 1;
        bowChargeInstance.transform.position = origin;
    }


    public override bool Update(){
        if(charging){
            charge += 60 * Time.deltaTime;
            Vector3 barPos = bowChargeInstance.transform.GetChild(0).transform.position;
            barPos.y = bowChargeInstance.transform.position.y + (charge / 100) * barTop / 2;
            bowChargeInstance.transform.GetChild(0).transform.position = barPos;
            
            Vector3 playePos = Player_Manager.player.transform.position;
            playePos.x += 1;
            playePos.y -= .5f;
            bowChargeInstance.transform.position = playePos;

            if(charge >= 100){
                charge = 100;
                charging = false;
            }
            return true;
        }
        return false;
    }
}
