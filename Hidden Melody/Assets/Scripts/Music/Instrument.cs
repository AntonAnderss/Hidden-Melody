using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Instrument", menuName = "Music Objects/Instrument", order = 0)]
public class Instrument : ScriptableObject
{
    [Header("Instrument Info")]
    public string instrumentName;

    [Header("Abilities")]
    public List<Ability> abilities;

}
