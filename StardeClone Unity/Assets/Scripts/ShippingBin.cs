using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingBin : Interactable
{

    public CanvasGroup menu;



    public void Start(){
        menu = GameObject.Find("Shipping Menu").GetComponent<CanvasGroup>();
    }
    public override void Interact(){

        menu.alpha = 1;
        Time_Manager.getInstance().Pause();
        menu.interactable = true;
        menu.blocksRaycasts = true;

    }
}
