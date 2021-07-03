using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokemonGOAP/ConfigData")]

public class ConfigData : ScriptableObject
{
    [Header("Global Settings")]
    public int FoodArrSize = 10;

}
