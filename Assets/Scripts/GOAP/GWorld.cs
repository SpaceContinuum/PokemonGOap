using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld {

    // Our GWorld instance
    private static readonly GWorld instance = new GWorld();
    // Our world states
    private static WorldStates world;
    // A list of freely roaming pokemon
    private static List<GameObject> freePokemon;
    // A list of freely eating pokemon
    private static List<GameObject> eatingPokemon;
    // A list of pokemon who fight
    private static List<GameObject> fightingPokemon;
    // A list of "dead" pokemon
    private static List<GameObject> stunnedPokemon;

    // A list of freely available food
    private static List<GameObject> freeFood;
    // A list of food that is being eaten right now
    private static List<GameObject> eatenFood;
    // A list of food that is fought over
    private static List<GameObject> fightFood;


    static GWorld() {

        // Create our world
        world = new WorldStates();
        // Create pokemon lists
        freePokemon =     new List<GameObject>();
        eatingPokemon =   new List<GameObject>();
        fightingPokemon = new List<GameObject>();
        stunnedPokemon =  new List<GameObject>();

        // Create food lists
        freeFood =  new List<GameObject>();
        eatenFood = new List<GameObject>();
        fightFood = new List<GameObject>();

        // Find all GameObjects that are tagged "Food"
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Food");
        // Then add them to the cubicles Queue
        foreach (GameObject f in fruits) {

            freeFood.Add(f);
        }

        // Inform the state
        if (fruits.Length > 0) {
            world.ModifyState("AvailableFood", fruits.Length);
        }

        // Find all GameObjects that are tagged "Food"
        GameObject[] pokemons = GameObject.FindGameObjectsWithTag("Pokemon");
        // Then add them to the cubicles Queue
        foreach (GameObject p in pokemons)
        {

            freePokemon.Add(p);
        }

        // Inform the state
        if (pokemons.Length > 0)
        {
            world.ModifyState("AvailablePokemon", pokemons.Length);
        }


        // Set the time scale in Unity
        Time.timeScale = 5.0f;

        
    }

    private GWorld() {

    }

    private bool MoveObjectFromTo(GameObject obj, List<GameObject> from, List<GameObject> to)
    {
        if (from.Contains(obj))
        {
            from.Remove(obj);
            to.Add(obj);
            return true;
        }

        return false;
    }

    public void PokemonFree2Eating(GameObject p)
    {
        MoveObjectFromTo(p, freePokemon, eatingPokemon);
    }

    public void PokemonFree2Fighting(GameObject p)
    {
        MoveObjectFromTo(p, freePokemon, fightingPokemon);
    }

    public void PokemonFighting2Stunned(GameObject p)
    {
        MoveObjectFromTo(p, fightingPokemon, stunnedPokemon);
    }

    public void PokemonFighting2Eating(GameObject p)
    {
        MoveObjectFromTo(p, fightingPokemon, eatingPokemon);
    }

    public void PokemonFighting2Free(GameObject p)
    {
        MoveObjectFromTo(p, fightingPokemon, freePokemon);
    }

    public void PokemonEating2Fighting(GameObject p)
    {
        MoveObjectFromTo(p, eatingPokemon, fightingPokemon);
    }

    public void PokemonEating2Free(GameObject p)
    {
        MoveObjectFromTo(p, eatingPokemon, freePokemon);
    }

    public void PokemonStunned2Free(GameObject p)
    {
        MoveObjectFromTo(p, stunnedPokemon, freePokemon);
    }

    // Remove Food
    public bool RemoveFood(GameObject f)
    {
        if (freeFood.Contains(f))
        {
            freeFood.Remove(f);
            return true;
        }

        else if (eatenFood.Contains(f))
        {
            eatenFood.Remove(f);
            return true;
        }

        else if (fightFood.Contains(f))
        {
            fightFood.Remove(f);
            return true;
        }
        return false;
    }

    // Add Count total food on the map
    public int FoodCounter()
    {
        int counter = 0;
        counter += (freeFood.Count + eatenFood.Count + fightFood.Count);
        return counter;
    }

    // Add Food
    public void AddNewFood(GameObject f)
    {
        freeFood.Add(f);
    }

    public void FoodFree2Eaten(GameObject f)
    {
        MoveObjectFromTo(f, freeFood, eatenFood);
    }

    public void FoodEaten2Fight(GameObject f)
    {
        MoveObjectFromTo(f, eatenFood, fightFood);
    }

    public void FoodFight2Eaten(GameObject f)
    {
        MoveObjectFromTo(f, fightFood, eatenFood);
    }

    private Food GetClosestFood(GameObject obj, List<GameObject> foodList)
    {
        Food closestFood = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = obj.transform.position;
        foreach (GameObject f in foodList)
        {
            float dist = Vector3.Distance(f.transform.position, currentPos);
            if (dist < minDistance)
            {
                closestFood = f.GetComponent<Food>();
                minDistance = dist;
            }
        }

        return closestFood;
    }

    public Food GetClosestFreeFood(GameObject obj)
    {
        return GetClosestFood(obj, freeFood);
    }

    Food GetClosestEatenFood(GameObject obj)
    {
        return GetClosestFood(obj, eatenFood);
    }

    public static GWorld Instance {

        get { return instance; }
    }

    public WorldStates GetWorld() {

        return world;
    }
}
