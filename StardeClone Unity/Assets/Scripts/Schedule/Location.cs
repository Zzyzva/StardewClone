using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location{



    public Vector3 position;
    public string scene;
    public int wait;

    public static Location sleep = new Location(new Vector3(0,0,0), "NULL");

    //Market
    public static Location center = new Location(new Vector3(-2,9,0), "Outside");
    public static Location leftStallPath = new Location(new Vector3(-8, 9, 0), "Outside");
    public static Location leftStall = new Location(new Vector3(-8, 5, 0), "Outside");

    //Church
    public static Location churchPath1 = new Location(new Vector3(-54, 28, 0), "Outside");
    public static Location churchPath2 = new Location(new Vector3(-62, 28, 0), "Outside");
    public static Location churchYard1 = new Location(new Vector3(-54, 32, 0), "Outside");
    public static Location churchYard2 = new Location(new Vector3(-52, 32, 0), "Outside");
    public static Location churchDoorOutside = new Location(new Vector3(-62, 32, 0), "Outside");
    public static Location churchDoorInside = new Location(new Vector3(0,-3,0), "Church");
    public static Location churchBack = new Location(new Vector3(0,5,0), "Church");
    public static Location churchRight = new Location(new Vector3(2,5,0), "Church");
    public static Location churchRoom = new Location(new Vector3(-7,5,0), "Church");

    //Herbalist Hut
    public static Location HerbalistHutPath = new Location(new Vector3(-65, 9, 0), "Outside");
    public static Location HerbalistHutDoorOutside = new Location(new Vector3(-65, 12, 0), "Outside");
    public static Location HerbalistHutDoorInside = new Location(new Vector3(0, -3, 0), "Herbalist Hut");
    public static Location HerbalistHutRoom = new Location(new Vector3(0, 0, 0), "Herbalist Hut");

    //Garden
    public static Location garden1 = new Location(new Vector3(-50, 0, 0), "Outside");
    public static Location garden2 = new Location(new Vector3(-50, -4, 0), "Outside");
    public static Location garden3 = new Location(new Vector3(-54, -4, 0), "Outside");
    public static Location garden4 = new Location(new Vector3(-54, 0, 0), "Outside");

    //Barracks
    public static Location barracksRoom = new Location(new Vector3(0, 5, 0), "Barracks");
    public static Location barracksDoorOutside = new Location(new Vector3(31, 12, 0), "Outside");
    public static Location barracksDoorInside = new Location(new Vector3(0,-3,0), "Barracks");
    public static Location barracksPath = new Location(new Vector3(31, 9, 0), "Outside");

    //Roads
    public static Location churchIntersection = new Location(new Vector3(-54, 9, 0), "Outside");
    public static Location northRoadEdge = new Location(new Vector3(-2, 38, 0), "Outside");
    public static Location westRoadEdge = new Location(new Vector3(-68, 9, 0), "Outside");
    public static Location houseRoadRightCorner = new Location(new Vector3(-2, -20, 0), "Outside");
    public static Location houseRoadLefttCorner = new Location(new Vector3(-39, -20, 0), "Outside");
    public static Location houseRoadTopCorner = new Location(new Vector3(-39, 9, 0), "Outside");
    
    public Location(Vector3 position, string scene){
        this.position = position;
        this.scene = scene;
        this.wait = 0;
    }

    public Location(Vector3 position, string scene, int wait){
        this.position = position;
        this.scene = scene;
        this.wait = wait;
    }

    //Adds a wait time and return itself
    //Makes declaration easier
    public Location withWait(int wait){
        return new Location(position, scene, wait);
    }

}

