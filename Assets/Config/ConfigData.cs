using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokemonGOAP/ConfigData")]

public class ConfigData : ScriptableObject
{
    [Header("Global Settings")]
    public int FoodArrSize = 10;
    public float DebugTextDuration = 3f;
    public int OccupiedFoodCostFactor=5;

    [Header("Pokemon Characteristics")]

    
    [Tooltip("The mean frequency (in seconds) for triggering Pokemon hunger action")]
    [Range(0.0F, 20.0F)]
    public float HungerFrequency = 30f;
    public float HungerVariance = 2f;

}
