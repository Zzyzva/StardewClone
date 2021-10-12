using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VendorInventory : MonoBehaviour
{


    [Serializable]
    public struct VendorItem {
        public Item item;
        public int price;   
    }
    public VendorItem[] inventory;
    public String standName;
}
