using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming_Manager : MonoBehaviour
{
    public static Farming_Manager instance;
    public List<Dirt> dirt;

    public int waterInCan = 2;
    public int maxWater = 2;

    private void Awake() {
        if(instance == null){
            instance = this;
            dirt = new List<Dirt>();  
        } else{
            Destroy(gameObject);
        }
    }



    public void NewDay(){
        foreach(Dirt tile in dirt){
            tile.NewDay();
        }
    }

    public void RefillCan(){
        waterInCan = maxWater + 1;
    }
}
