using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToFreeWeakTypeOpponent : GoToFreeOpponent
{

    public new void Start() {
        p = gameObject.GetComponent<Pokemon>();
        myType = p.GetMyPokemonType();
        myWeaknessType = p.GetMyWeaknessType();
        myStrengthType = p.GetMyStrengthType();

        //TODO: verify what this nmeeds to be
        //targetType = strengthType;
        targetType = myStrengthType;

    }
    public override void Reset()
    {
        
        duration = 5;
        actionName = "FindFreeWeakTypeOpponent";
        
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState(WorldState.Label.availablePokemon, 0);
        preConditions[1] = new WorldState("isViolent", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("attacking", 0);

    }

}