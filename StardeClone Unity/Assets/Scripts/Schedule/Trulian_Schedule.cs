using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trulian_Schedule : Schedule
{

    public override void childStart(){
        start = Location.churchRoom;
    }

    public override void UpdateSchedule(int hour, int minute, string meridiem){
        //Go to Church
        if(hour == 8 && minute == 0 && meridiem == "am"){
            queue.Enqueue(Location.churchBack);
            
        }

        //Watch Sunset
        if(hour == 4 && minute == 0 && meridiem == "pm"){
            queue.Enqueue(Location.churchDoorInside);
            queue.Enqueue(Location.churchDoorOutside);
            queue.Enqueue(Location.churchPath2);
            queue.Enqueue(Location.churchPath1);
            queue.Enqueue(Location.churchYard1);
            queue.Enqueue(Location.churchYard2);   
        }

        //Go to bed
        if(hour == 8 && minute == 0 && meridiem == "pm"){

            queue.Enqueue(Location.churchYard1);
            queue.Enqueue(Location.churchPath1);
            queue.Enqueue(Location.churchPath2);
            queue.Enqueue(Location.churchDoorOutside);
            queue.Enqueue(Location.churchDoorInside);
            queue.Enqueue(Location.churchBack);
            queue.Enqueue(Location.churchRoom);
        }
        
    }
}
