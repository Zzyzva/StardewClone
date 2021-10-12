using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    public Transform itemsParent;
    public Slider slider;
    public Text goldText;

    public Inventory_Slot[] slots;
    public List<Item> inventory = new List<Item>();
    public List<Item> toSell = new List<Item>();
    private int maxInvetory = 10;
    public static Inventory_Manager instance;
    private int selectedSlot = 0;


    public List<Item> startInventory = new List<Item>();

    public float energy;
    public float maxEnergy = 100;

    public int gold = 500;

    public bool canChangeSelected =true;

    



    private void Awake() {
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<Inventory_Slot>();
        foreach( Inventory_Slot slot in slots){
            slot.Unselect();
        }
        slots[0].Select();

        foreach(Item item in startInventory){
            AddItem(item, 1);
        }

        energy = maxEnergy;
        slider.maxValue = maxEnergy;
        slider.value = energy;
        

    }

    // Update is called once per frame
    void Update()
    {
        //Change highlight
         #region INPUT
        
        if(!Time_Manager.getInstance().pause && canChangeSelected){
           
            if(Input.GetKeyDown("1"))
                ChangeSelected(0);

            if(Input.GetKeyDown("2"))
                ChangeSelected(1);

            if(Input.GetKeyDown("3"))
                ChangeSelected(2);

            if(Input.GetKeyDown("4"))
                ChangeSelected(3);

            if(Input.GetKeyDown("5"))
                ChangeSelected(4);

            if(Input.GetKeyDown("6"))
                ChangeSelected(5);

            if(Input.GetKeyDown("7"))
                ChangeSelected(6);

            if(Input.GetKeyDown("8"))
                ChangeSelected(7);

            if(Input.GetKeyDown("9"))
                ChangeSelected(8);

            if(Input.GetKeyDown("0"))
                ChangeSelected(9);
            
        }
        #endregion
    


        //Check for using item
        if(!Time_Manager.getInstance().pause && Input.GetMouseButtonDown(1)){
            if(slots[selectedSlot].item){
                slots[selectedSlot].item.OnUse();

            }
        }
        if(!Time_Manager.getInstance().pause && Input.GetMouseButtonUp(1)){
            if(slots[selectedSlot].item){
                slots[selectedSlot].item.OnUseUp();
            }
        }

        if(slots[selectedSlot].item){
            slots[selectedSlot].item.Update();
        }
    




        //Check Energy
        slider.value = energy;
        if(energy <= 0){
            Time_Manager.getInstance().Sleep();
            energy = maxEnergy;
        }

        //Check gold
        goldText.text = gold + " g";
    
    
    }

    void ChangeSelected(int select){
        if(Player_Manager.player.GetComponent<Player_Movement>().canMove){
            slots[selectedSlot].Unselect();
            slots[select].Select();
            selectedSlot = select;
        }
            
    }

    public void AddItem(Item item, int count){
        if(inventory.Count < maxInvetory){
            if(inventory.Contains(item)){
                item.count += count;
                item.slot.SetItem(item);
            } else{
                item.count = count;
                foreach(Inventory_Slot slot in slots){
                    if(!slot.item){
                        slot.SetItem(item);
                        item.slot = slot;
                        break;
                    }
                }
                inventory.Add(item);
            }    
        }
    }

    public bool RemoveItem(Item item, int count){
        if(inventory.Contains(item)){
            if(item.count - count < 0){
                return false;
            }
            item.count -= count;
            item.slot.SetItem(item);
            if(item.count == 0){
                inventory.Remove(item);
                item.slot.item = null;
                item.slot.ClearSlot();
            }
            
            return true;
        }
        return false;
    }

    public bool RemoveItemByName(string name, int count){
        foreach(Item item in inventory){
            if(item.name == name){
                RemoveItem(item, count);
                break;
            }
        }
        return false;
    }


    public void SellItem(Item item){
        toSell.Add(item);
        inventory.Remove(item);     
    }
}
