using System.Collections.Generic;
using UnityEngine;


public class HitCombination 
{



    public List<HitZone> zones;
    public string combinationName;

    // Blueprint for combinations
    public HitCombination(string name, List<HitZone> hitZones)
    {
        combinationName = name;    
        zones = hitZones;
        
    }
    
}
