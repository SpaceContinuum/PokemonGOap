using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToFreeSameTypeOpponent : GoToFreeOpponent
{

    public override void Reset()
    {
        p = gameObject.GetComponent<Pokemon>();
        duration = 5;
        actionName = "FindFreeSameTypeOpponent";
        myType = p.GetPokemonType();
        weaknessType = p.GetWeaknessType();
        strengthType = p.GetStrengthType();

        targetType = myType;

        preConditions = new WorldState[2];
        preConditions[0] = new WorldState("availablePokemon", 0);
        preConditions[1] = new WorldState("isViolent", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("fighting", 0);

    }
}
