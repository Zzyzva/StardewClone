using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sophia_Schedule : Schedule
{


    public override void childStart(){
        start = Location.center;
        
    }

    public override void UpdateSchedule(int hour, int minute, string meridiem){
        //Go to Church
        if(hour == 8 && minute == 0 && meridiem == "am"){
            queue.Enqueue(Location.churchIntersection);
            queue.Enqueue(Location.churchPath1);
            queue.Enqueue(Location.churchPath2);
            queue.Enqueue(Location.churchDoorOutside);
            queue.Enqueue(Location.churchDoorInside);
            queue.Enqueue(Location.churchBack);
            queue.Enqueue(Location.churchRight);
        }

        //Go outside
        if(hour == 2 && minute == 10 && meridiem == "pm"){
            queue.Enqueue(Location.churchBack);
            queue.Enqueue(Location.churchDoorInside);
            queue.Enqueue(Location.churchDoorOutside);
            queue.Enqueue(Location.churchPath2);
            queue.Enqueue(Location.churchPath1);
            queue.Enqueue(Location.churchIntersection);
            queue.Enqueue(Location.center);
            
            
            
        }
        
    }

    

    
}
