using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorStall : Interactable
{

    public CanvasGroup vendorMenu;

    public void Start(){
        vendorMenu = Vendor_Manager.instance.vendorMenu;
    }


    public override void Interact() {
        Time_Manager.getInstance().Pause();
        Vendor_Manager.instance.loadLeftStall();
        vendorMenu.alpha = 1f;
        vendorMenu.interactable = true;
        vendorMenu.blocksRaycasts = true;
    }
}
