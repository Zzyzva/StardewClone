using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public enum Type{
        lake,
        river
    }
   
   public Type type;
   public void WaterHit(){
       Farming_Manager.instance.RefillCan();
   }
}
