using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crop : MonoBehaviour
{
   public Sprite seeds;

   [Serializable]
    public struct GrowthPeriod {
        public Sprite sprite;
        public int day;
    }

    public List<GrowthPeriod> growth;

    public Item item;
}
