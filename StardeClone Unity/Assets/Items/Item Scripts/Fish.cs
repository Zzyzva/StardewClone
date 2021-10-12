using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    public int speed;
    public int probability;
    public enum FishType {random, dash};
    public FishType type;

    public List<Water.Type> waterTypes = new List<Water.Type>();
    public float size = 1;
}
