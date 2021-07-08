using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToEatingWeakTypeOpponent : GoToEatingOpponent
{

    public new void Start() {
        
        
        p = gameObject.GetComponent<Pokemon>();
        myType = p.GetPokemonType();
        weaknessType = p.GetWeaknessType();
        strengthType = p.GetStrengthType();

        targetType = weaknessType;
    }
    public override void Reset()
    {
        
        duration = 5;
        actionName = "FindEatingeWeakTypeOpponent";
       

        preConditions = new WorldState[2];
        //preConditions[0] = new WorldState("availablePokemon", 0);
        preConditions[0] = new WorldState(WorldState.Label.isViolent, 0);
        preConditions[1] = new WorldState(WorldState.Label.eatingPokemon, 0);
        afterEffects = new WorldState[2];
        afterEffects[0] = new WorldState(WorldState.Label.attacking, 1);
        afterEffects[1] = new WorldState(WorldState.Label.attackingForFood, 1);

    }
}
