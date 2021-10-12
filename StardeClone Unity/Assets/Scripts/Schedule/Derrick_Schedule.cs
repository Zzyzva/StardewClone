using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derrick_Schedule : Schedule
{


    public override void childStart(){
        start = Location.barracksRoom;
        
    }

    public override void UpdateSchedule(int hour, int minute, string meridiem){
        //Patrol town
        if(hour == 8 && minute == 0 && meridiem == "am"){
            queue.Enqueue(Location.barracksDoorInside);
            queue.Enqueue(Location.barracksDoorOutside);
            queue.Enqueue(Location.barracksPath);
            queue.Enqueue(Location.center);
            queue.Enqueue(Location.northRoadEdge);
            queue.Enqueue(Location.houseRoadRightCorner);
            queue.Enqueue(Location.houseRoadLefttCorner);
            queue.Enqueue(Location.houseRoadTopCorner);
            queue.Enqueue(Location.westRoadEdge);
            queue.Enqueue(Location.barracksPath);
            queue.Enqueue(Location.barracksDoorOutside);
            queue.Enqueue(Location.barracksDoorInside);
            queue.Enqueue(Location.barracksRoom);
        }

        
        
    }

    

    
}
