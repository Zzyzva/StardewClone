using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dirt : Interactable
{


    public bool tilled = false;
    public bool watered = false;
    public bool planted = false;
    public bool ready = false;

    public Sprite tilledDirt;
    public Sprite wateredDirt;

    public GameObject readySprite;


    Crop crop;
    Crop.GrowthPeriod period;
    int periodPosition;
    int growthDays;

    void Start(){
        Farming_Manager.instance.dirt.Add(this);
    }




    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Outside"){
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        } else{
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    // Start is called before the first frame update


    public void HoeHit(){
            tilled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = tilledDirt;

    }

    public void WaterHit(){
        if(tilled && Farming_Manager.instance.waterInCan > 0){
            watered = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = wateredDirt;
        }
    }

    public void NewDay(){
        if(watered){
            watered = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = tilledDirt;
            if(planted && !ready){
                growthDays++;
                if(growthDays == period.day){
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = period.sprite;
                    periodPosition++;
                    if(periodPosition == crop.growth.Count){
                        ready = true;
                        readySprite.SetActive(true);
                    }else{
                        period = crop.growth[periodPosition];
                    }
                    
                    
                }
            }
        }
        
    }

    public bool PlantSeeds(Crop crop){
        if(!planted && tilled){
            planted = true;
            this.crop = crop;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = crop.seeds;
            period = crop.growth[0];
            periodPosition = 0;
            growthDays = 0;
            return true;
        }
        return false;
        
    }

    public override void Interact(){
        if(ready){
            Inventory_Manager.instance.AddItem(crop.item, 1);
            readySprite.SetActive(false);
            ready = false;
            planted = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
