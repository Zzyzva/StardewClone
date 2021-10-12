using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor_Manager : MonoBehaviour
{

    public static Vendor_Manager instance;
    public VendorInventory leftStallInventory;
    


    public CanvasGroup vendorMenu;
    public Text title;
    public GameObject vendorMenuContent;
    public GameObject vendorUIPrefab;

    private void Awake() {
        if(instance){
            Destroy(this);
        } else{
            instance = this;
        }
    }


    public void SetLeftStall(VendorInventory inventory){
        leftStallInventory = inventory;
        title.text = inventory.standName;

    }


    public void loadLeftStall(){
        //Destorys old quests on the board
        for(int i = 0; i < vendorMenuContent.transform.childCount; i++){
            Destroy(vendorMenuContent.transform.GetChild(i).gameObject);
        }
        //Loads new quests
        if(leftStallInventory){
            foreach(VendorInventory.VendorItem item in leftStallInventory.inventory){
                GameObject UI = Instantiate(vendorUIPrefab);
                UI.transform.transform.parent = vendorMenuContent.transform;
                Text text = UI.transform.GetChild(1).GetComponent<Text>();
                text.text = item.item.name + "   " + item.price + "g";
                UI.transform.GetChild(2).GetComponent<BuyItemButton>().item = item;
                Image icon = UI.transform.GetChild(3).GetComponent<Image>();
                icon.sprite = item.item.icon;
            }
        }
        
        
    }


}
