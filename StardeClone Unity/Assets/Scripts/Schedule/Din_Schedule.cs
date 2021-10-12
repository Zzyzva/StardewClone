using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Din_Schedule : Schedule
{


    public override void childStart(){
        start = Location.HerbalistHutRoom;
        
    }

    public override void UpdateSchedule(int hour, int minute, string meridiem){





        //Walk to stall
        if(hour == 7 && minute == 30 && meridiem == "am"){

            Vendor_Manager.instance.SetLeftStall(gameObject.GetComponent<VendorInventory>());
            
            queue.Enqueue(Location.HerbalistHutDoorInside);
            queue.Enqueue(Location.HerbalistHutDoorOutside);
            queue.Enqueue(Location.HerbalistHutPath);
            queue.Enqueue(Location.leftStallPath);
            queue.Enqueue(Location.leftStall);

        }

        //Open up shop
        if(hour == 10 && minute == 0 && meridiem == "am"){

        }


        //Close Shop
        if(hour == 5 && minute == 0 && meridiem == "pm"){

        }


        //Explore the garden
        if(hour == 5 && minute == 30 && meridiem == "pm"){
            queue.Enqueue(Location.garden2);
            queue.Enqueue(Location.garden3.withWait(30));
            queue.Enqueue(Location.garden4.withWait(30));
            queue.Enqueue(Location.garden1.withWait(30));
            queue.Enqueue(Location.garden2.withWait(30));
            queue.Enqueue(Location.garden3.withWait(30));
            queue.Enqueue(Location.garden4.withWait(30));
        }

        
        
    }
}
